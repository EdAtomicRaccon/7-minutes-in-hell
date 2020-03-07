using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.PubSub;
#endif // !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL

// TODO: create async connection workflow (async method or coroutine?)
public class TwitchExtensionGameClient : Singleton<TwitchExtensionGameClient>
{
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL

    [Header("Application Settings")]
    public string clientID;
    public string redirectURI;

    [Header("Authentication")]
    public string username;
    public string accessToken; // OAUTH token used for authentication
    public TwitchScope scope;

    [Header("Connection")]
    public string channelname;
    public string channelId;

    private Client client;
    public Client Client { get { return client; } }
    private TwitchPubSub twitchPubSub;
    public TwitchPubSub TwitchPubSub { get { return twitchPubSub; } }

    bool runInBackground;

    private bool isConnecting = false;
    private bool clientConnected = false;
    private bool pubSubConnected = false;
    private bool pubSubListening = false;
    private bool channelIdReceived = false;
    private bool connected = false;

    public bool IsConnecting { get { return isConnecting; } }
    public bool IsConnected { get { return connected; } }

#region Unity Messages

    private void OnEnable()
    {
        runInBackground = Application.runInBackground;
        Application.runInBackground = true;

        client = new Client();

        // hook up events
        //client.OnBeingHosted += Client_OnBeingHosted;
        client.OnChatCommandReceived += Client_OnChatCommandReceived;
        client.OnConnected += Client_OnConnected;
        client.OnConnectionError += Client_OnConnectionError;
        client.OnDisconnected += Client_OnDisconnected;
        client.OnGiftedSubscription += Client_OnGiftedSubscription;
        client.OnIncorrectLogin += Client_OnIncorrectLogin;
        client.OnJoinedChannel += Client_OnJoinedChannel;
        client.OnLeftChannel += Client_OnLeftChannel;
        client.OnMessageReceived += Client_OnMessageReceived;
        client.OnNewSubscriber += Client_OnNewSubscriber;
        client.OnRaidNotification += Client_OnRaidNotification;
        client.OnReSubscriber += Client_OnReSubscriber;
        client.OnUserJoined += Client_OnUserJoined;
        client.OnWhisperCommandReceived += Client_OnWhisperCommandReceived;


        twitchPubSub = new TwitchPubSub();

        twitchPubSub.OnPubSubServiceConnected += TwitchPubSub_OnPubSubServiceConnected;
        twitchPubSub.OnListenResponse += TwitchPubSub_OnListenResponse;
        twitchPubSub.OnBitsReceived += TwitchPubSub_OnBitsReceived;
        twitchPubSub.OnFollow += TwitchPubSub_OnFollow;
        twitchPubSub.OnChannelSubscription += TwitchPubSub_OnChannelSubscription;

        //// authenticate and connect

        //// get access token
        //if (string.IsNullOrEmpty(accessToken))
        //{
        //    accessToken = PlayerPrefs.GetString("TwitchOauthToken", string.Empty);
        //    if (string.IsNullOrEmpty(accessToken))
        //    {
        //        // initiate token generation
        //        Application.OpenURL(GetRequestString());
        //    }
        //} else
        //{
        //    // try to connect
        //    InitializeClient();

        //    ConnectClient();

        //    twitchPubSub.Connect();
        //}
    }


    private void OnDisable()
    {
        //client.OnBeingHosted -= Client_OnBeingHosted;
        client.OnChatCommandReceived -= Client_OnChatCommandReceived;
        client.OnConnected -= Client_OnConnected;
        client.OnConnectionError -= Client_OnConnectionError;
        client.OnDisconnected -= Client_OnDisconnected;
        client.OnGiftedSubscription -= Client_OnGiftedSubscription;
        client.OnIncorrectLogin -= Client_OnIncorrectLogin;
        client.OnJoinedChannel -= Client_OnJoinedChannel;
        client.OnLeftChannel -= Client_OnLeftChannel;
        client.OnMessageReceived -= Client_OnMessageReceived;
        client.OnNewSubscriber -= Client_OnNewSubscriber;
        client.OnRaidNotification -= Client_OnRaidNotification;
        client.OnReSubscriber -= Client_OnReSubscriber;
        client.OnUserJoined -= Client_OnUserJoined;
        client.OnWhisperCommandReceived -= Client_OnWhisperCommandReceived;

        // disconnect
        DisconnectClient();

        twitchPubSub.OnPubSubServiceConnected -= TwitchPubSub_OnPubSubServiceConnected;
        twitchPubSub.OnListenResponse -= TwitchPubSub_OnListenResponse;
        twitchPubSub.OnBitsReceived -= TwitchPubSub_OnBitsReceived;
        twitchPubSub.OnFollow -= TwitchPubSub_OnFollow;
        twitchPubSub.OnChannelSubscription -= TwitchPubSub_OnChannelSubscription;

        Application.runInBackground = runInBackground;

        clientConnected = false;
        pubSubConnected = false;
        pubSubListening = false;
        channelIdReceived = false;
        connected = false;
    }

    //private void OnGUI()
    //{
    //    if (client == null) return;

    //    GUILayout.Label($"{client.TwitchUsername} : {client.IsConnected} \n" +
    //        $"{client.JoinedChannels.Count}");
    //    foreach (var channel in client.JoinedChannels)
    //    {
    //        GUILayout.Label($"{channel.Channel}");
    //    }
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        SendDebugMessage();
    //    }
    //}

#endregion


#region Event Handler

    private void Client_OnWhisperCommandReceived(object sender, TwitchLib.Client.Events.OnWhisperCommandReceivedArgs e)
    {
        Debug.Log($"Received whisper command {e.Command.ToString()}");
    }

    private void Client_OnUserJoined(object sender, TwitchLib.Client.Events.OnUserJoinedArgs e)
    {
        Debug.Log($"{e.Username} joined channel {e.Channel}");
    }

    private void Client_OnReSubscriber(object sender, TwitchLib.Client.Events.OnReSubscriberArgs e)
    {
        Debug.Log($"{e.ReSubscriber.DisplayName} resubscribed to channel {e.Channel} for a total of {e.ReSubscriber.Months} months");
    }

    private void Client_OnRaidNotification(object sender, TwitchLib.Client.Events.OnRaidNotificationArgs e)
    {
        Debug.Log($"{e.RaidNotificaiton.MsgParamDisplayName} raids channel {e.Channel} with {e.RaidNotificaiton.MsgParamViewerCount} viewers");
    }

    private void Client_OnNewSubscriber(object sender, TwitchLib.Client.Events.OnNewSubscriberArgs e)
    {
        Debug.Log($"{e.Subscriber.DisplayName} just subscribed to channel {e.Channel} for the first time");
    }

    // TODO: the parameter has info about the type of subscription (gifted, prime, etc.) - let's react to those 
    private void TwitchPubSub_OnChannelSubscription(object sender, TwitchLib.PubSub.Events.OnChannelSubscriptionArgs e)
    {
        Debug.Log($"{e.Subscription.DisplayName} just subscribed to channel {e.Subscription.ChannelName}");
    }

    private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
    {
        Debug.Log($"Chat | {e.ChatMessage.DisplayName}: {e.ChatMessage.Message}");
    }

    private void Client_OnGiftedSubscription(object sender, TwitchLib.Client.Events.OnGiftedSubscriptionArgs e)
    {
        Debug.Log($"{e.GiftedSubscription.DisplayName} just gifted {e.GiftedSubscription.MsgParamRecipientDisplayName} a subscription to channel {e.Channel} ({e.GiftedSubscription.MsgParamMonths} months)");
    }

    private void Client_OnDisconnected(object sender, TwitchLib.Client.Events.OnDisconnectedArgs e)
    {
        Debug.Log($"{e.BotUsername} disconnected");
        clientConnected = false;
    }

    private void Client_OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
    {
        Debug.Log($"Chat command | {e.Command.CommandText} {e.Command.ArgumentsAsString}");
    }

    private void Client_OnBeingHosted(object sender, TwitchLib.Client.Events.OnBeingHostedArgs e)
    {
        Debug.Log($"{e.BeingHostedNotification.HostedByChannel} ist now hosting channel {e.BeingHostedNotification.Channel}");
    }

    private void Client_OnIncorrectLogin(object sender, TwitchLib.Client.Events.OnIncorrectLoginArgs e)
    {
        throw e.Exception;
    }

    private void Client_OnLeftChannel(object sender, TwitchLib.Client.Events.OnLeftChannelArgs e)
    {
        Debug.Log($"Left channel {e.Channel}");
    }

    private void Client_OnJoinedChannel(object sender, TwitchLib.Client.Events.OnJoinedChannelArgs e)
    {
        Debug.Log($"Joined channel {e.Channel}");
    }

    private void Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
    {
        Debug.Log($"Connected with user {e.BotUsername}");
        clientConnected = true;
    }

    private void Client_OnConnectionError(object sender, TwitchLib.Client.Events.OnConnectionErrorArgs e)
    {
        throw e.Error.Exception;
    }

    private void TwitchPubSub_OnFollow(object sender, TwitchLib.PubSub.Events.OnFollowArgs e)
    {
        Debug.Log($"{e.DisplayName} is now following channel {e.FollowedChannelId}");
    }

    private void TwitchPubSub_OnBitsReceived(object sender, TwitchLib.PubSub.Events.OnBitsReceivedArgs e)
    {
        Debug.Log($"{e.Username} just cheered {e.BitsUsed} bits. ({e.TotalBitsUsed} total)");
    }

    private void TwitchPubSub_OnListenResponse(object sender, TwitchLib.PubSub.Events.OnListenResponseArgs e)
    {
        if (e.Successful)
        {
            Debug.Log($"OnListenResponse: {e.Response.Nonce}");
            pubSubListening = true;
        }
        else
        {
            Debug.Log($"OnListenResponse: {e.Response.Error}");
        }
    }

    private void TwitchPubSub_OnPubSubServiceConnected(object sender, System.EventArgs e)
    {
        Debug.Log($"PubSubService connected {e}");
        pubSubConnected = true;
    }

    private void OnChannelIdReceived(string userId)
    {
        channelId = userId;
        channelIdReceived = true;
    }

    #endregion


    #region Private Functions

#if ODIN_INSPECTOR
    [Button]
#endif
    private void InitializeClient()
    {
        ConnectionCredentials credentials = new ConnectionCredentials(username, accessToken);
        client.Initialize(credentials, channelname);
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    private void ConnectClient()
    {
        client.Connect();
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    private void ClientJoinChannel()
    {
        client.JoinChannel(channelname);
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    private void ClientLeaveChannel()
    {
        client.LeaveChannel(channelname);
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    private void SendDebugMessage()
    {
        client.SendMessage(channelname, "Hello world!");
        client.SendRaw("Hello raw!");
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    private void DisconnectClient()
    {
        client.Disconnect();
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    private void GetChannelIdViaCoroutine()
    {
        StartCoroutine(GetChannelId(accessToken, OnChannelIdReceived));
    }

    string GetRequestString()
    {
        string request = "https://id.twitch.tv/oauth2/authorize";

        request += $"?client_id={clientID}";
        request += $"&redirect_uri={redirectURI}";
        request += $"&response_type=token";
        request += $"&scope={scope.ToQueryString()}";

        return request;
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    private void PubSubStartListening()
    {
        twitchPubSub.ListenToFollows(channelId);
        twitchPubSub.ListenToBitsEvents(channelId);
        twitchPubSub.ListenToSubscriptions(channelId);

        twitchPubSub.SendTopics(accessToken);
    }

    IEnumerator GetChannelId(string oauthToken, System.Action<string> onSuccess)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"https://api.twitch.tv/kraken?api_version=5&oauth_token={oauthToken}"))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string response = request.downloadHandler.text;
                string pattern = "(?:\"user_id\":\")(.*?)(?:\")";
                if (Regex.IsMatch(response, pattern))
                {
                    var match = Regex.Match(response, pattern);
                    if (match.Groups.Count > 1)
                    {
                        string userId = match.Groups[1].Value;
                        Debug.Log($"Received RegEx match from json response string. UserId = {userId}");
                        onSuccess?.Invoke(userId);
                    }
                }
            }
        }
    }

    IEnumerator InitializeConnection(System.Action onSuccess)
    {
        clientConnected = false;
        pubSubConnected = false;
        pubSubListening = false;
        channelIdReceived = false;
        connected = false;

        if (string.IsNullOrEmpty(username))
        {
            username = PlayerPrefs.GetString("TwitchUsername", string.Empty);
            if (string.IsNullOrEmpty(username))
            {
                Debug.LogError("No username specified. Aborting connection atempt");
                yield break;
            }
            channelname = username;
        }

        if (string.IsNullOrEmpty(accessToken))
        {
            accessToken = PlayerPrefs.GetString("TwitchOauthToken", string.Empty);
            if (string.IsNullOrEmpty(accessToken))
            {
                Debug.LogError("No access token specified. Aborting connection atempt");
                yield break;
            }
        }

        isConnecting = true;
        // try to connect
        InitializeClient();
        ConnectClient();
        while (!clientConnected) yield return null;

        PubSubConnect();
        while (!pubSubConnected) yield return null;

        yield return GetChannelId(accessToken, OnChannelIdReceived);
        while (!channelIdReceived) yield return null;
        PubSubStartListening();
        while (!pubSubListening) yield return null;

        // store access token in player prefs
        PlayerPrefs.SetString("TwitchOauthToken", accessToken);


        isConnecting = false;
        connected = true;

        onSuccess?.Invoke();
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    private void PubSubConnect()
    {
        twitchPubSub.Connect();
    }

#endregion

    #region Public Functions

    public void Connect(System.Action onSuccess)
    {
        if (!IsConnecting)
        {
            StartCoroutine(InitializeConnection(onSuccess));
        }
    }

    public void AbortConnection()
    {
        if (IsConnecting)
        {
            StopAllCoroutines();
            OnDisable();
        }
    }

    /// <summary>
    /// this gets called when a new connection is set up
    /// </summary>
    /// <param name="username"></param>
    public void OnUsernameEntered(string username)
    {
        this.username = username;
        this.channelname = username;
    }

    public void RequestAccessToken()
    {
        // request access token for user
        Application.OpenURL(GetRequestString());
    }

    public void OnTokenEntered(string token)
    {
        this.accessToken = token;
    }

    #endregion

#endif // !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL
}
