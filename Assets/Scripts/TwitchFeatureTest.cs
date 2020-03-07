using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib;

public class TwitchFeatureTest : MonoBehaviour
{
    private void OnEnable()
    {
        TwitchExtensionGameClient.Instance.Client.OnMessageReceived += Client_OnMessageReceived;
    }
    private void OnDisable()
    {
        TwitchExtensionGameClient.Instance.Client.OnMessageReceived -= Client_OnMessageReceived;
    }

    private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
    {
        Debug.Log($"Message received from {e.ChatMessage.DisplayName}: {e.ChatMessage.Message}");
    }
}
