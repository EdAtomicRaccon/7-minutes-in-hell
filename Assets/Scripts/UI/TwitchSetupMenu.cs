using UnityEngine;
using System.Collections;

public class TwitchSetupMenu : MonoBehaviour
{
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
}
