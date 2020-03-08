using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TwitchSetupMenu : MonoBehaviour
{
    public UnityEvent OnConnectionSuccessEvent;

    private void OnEnable()
    {
        TwitchExtensionGameClient.Instance.Client.OnConnected += Client_OnConnected;   
    }

    private void OnDisable()
    {
        TwitchExtensionGameClient.Instance.Client.OnConnected -= Client_OnConnected;
    }

    #region Menu Functions

    public void OnUsernameEntered(string user)
    {
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL
        TwitchExtensionGameClient.Instance.OnUsernameEntered(user);
        TwitchExtensionGameClient.Instance.RequestAccessToken();
#endif
    }

    public void OnUsernameCanceled()
    {
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL
#endif
    }

    public void OnChannelnameEntered(string channel)
    {
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL
        TwitchExtensionGameClient.Instance.OnChannelnameEntered(channel);
#endif
    }

    public void OnChannelnameCanceled()
    {
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL
#endif
    }

    public void OnAccessTokenEntered(string token)
    {
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL
        TwitchExtensionGameClient.Instance.OnTokenEntered(token);
        TwitchExtensionGameClient.Instance.Connect(OnConnectionSuccess);
#endif
    }

    public void OnAccessTokenCanceled()
    {
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL
#endif
    }

    void OnConnectionSuccess()
    {
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_SWITCH && !UNITY_TVOS && !UNITY_WEBGL
        Debug.Log("Successfully linked Twitch account");
#endif
    }
    #endregion

    #region MyRegion

    private void Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
    {
        OnConnectionSuccessEvent?.Invoke();
    }

    #endregion
}
