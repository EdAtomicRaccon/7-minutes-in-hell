using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[System.Serializable]
public class TwitchMessageEntry
{
    public float time;
    public TwitchLib.Client.Models.ChatMessage message;

    public TwitchMessageEntry(TwitchLib.Client.Models.ChatMessage message)
    {
        time = Time.realtimeSinceStartup;
        this.message = message;
    }
}

[System.Serializable]
public class CommunityChoiceData
{
    public int votesA, votesB, votesTotal;

    public float ChoiceA => ((votesTotal > 0) ? ((float)votesA / votesTotal) : 0);
    public float ChoiceB => ((votesTotal > 0) ? ((float)votesB / votesTotal) : 0);
    public TwitchMessageEntry[] messages;

    public CommunityChoiceData(int votesA, int votesB, List<TwitchMessageEntry> messages)
    {
        this.votesA = votesA;
        this.votesB = votesB;
        this.votesTotal = votesA + votesB;
        this.messages = messages.ToArray();
    }
}

public class TwitchGameLogic : Singleton<TwitchGameLogic>
{
    public TwitchLib.Unity.Client Client => TwitchExtensionGameClient.Instance.Client;

    public UnityEvent OnVoteReceived;

    List<TwitchMessageEntry> allMessagesList;


    // current round
    bool isVoting = false;
    float timeAtStartOfRound;
    List<TwitchMessageEntry> messagesOfCurrentRound;
    List<string> userIdAlreadyVotedList;
    int currentVotesA, currentVotesB, currentVotesTotal;
    string matchStringOptionA, matchStringOptionB;

    [ShowInInspector]
    public float CurrentChoiceA => (currentVotesTotal > 0) ? ((float)currentVotesA / currentVotesTotal) : 0;
    [ShowInInspector]
    public float CurrentChoiceB => (currentVotesTotal > 0) ? ((float)currentVotesB / currentVotesTotal) : 0;
    public float TimeAtStartOfRound => timeAtStartOfRound;
    [ShowInInspector]
    public float CurrentRoundTime => (Time.realtimeSinceStartup - timeAtStartOfRound);
    public bool IsVoting => isVoting;


    #region Unity Messages

    private void OnEnable()
    {
        Client.OnMessageReceived += Client_OnMessageReceived;

        allMessagesList.Clear();
    }

    private void Awake()
    {
        allMessagesList = new List<TwitchMessageEntry>();
        messagesOfCurrentRound = new List<TwitchMessageEntry>();
        userIdAlreadyVotedList = new List<string>();
    }

    private void OnDisable()
    {
        if (Client == null) return;

        Client.OnMessageReceived -= Client_OnMessageReceived;
    }

    #endregion


    #region Public Round Control

    [Button]
    public void StartVoting(string optionA = "1", string optionB = "2")
    {
        isVoting = true;
        timeAtStartOfRound = Time.realtimeSinceStartup;
        messagesOfCurrentRound.Clear();
        userIdAlreadyVotedList.Clear();
        currentVotesA = 0;
        currentVotesB = 0;
        currentVotesTotal = 0;
        this.matchStringOptionA = optionA;
        this.matchStringOptionB = optionB;

        SendChatMessage($"Voting has started. Send a message beginning with '{optionA}' or '{optionB}' to make your choice.");
    }

    public void SendTimerMessage(int secondsLeft)
    {
        SendMessage($"Voting will end in {secondsLeft} seconds.");
    }

    [Button]
    public CommunityChoiceData EndVoting()
    {
        isVoting = false;
        SendChatMessage($"Voting has ended. '{matchStringOptionA}': {currentVotesA} ({CurrentChoiceA.ToPercentageString()}), '{matchStringOptionB}': {currentVotesB} ({CurrentChoiceB.ToPercentageString()}), total number of votes: {currentVotesTotal}");
        return new CommunityChoiceData(currentVotesA, currentVotesB, messagesOfCurrentRound);
    }

    #endregion


    #region Helper

    void SendChatMessage(string message)
    {
        Client.SendMessage(TwitchExtensionGameClient.Instance.channelname, message);
    }

    #endregion


    #region Event Listener

    private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
    {
        allMessagesList.Add(new TwitchMessageEntry(e.ChatMessage));

        if (isVoting)
        {
            // can user still vote
            if (!userIdAlreadyVotedList.Contains(e.ChatMessage.UserId))
            {
                // parse message
                string message = e.ChatMessage.Message.ToLower();

                string[] messageSplit = message.Split(' ');

                if (string.Compare(matchStringOptionA.ToLower(), messageSplit[0]) == 0)
                {
                    // voted for A
                    currentVotesA++;
                    currentVotesTotal++;
                    userIdAlreadyVotedList.Add(e.ChatMessage.UserId);
                    OnVoteReceived?.Invoke();
                } else if (string.Compare(matchStringOptionB.ToLower(), messageSplit[0]) == 0)
                {
                    // voted for B
                    currentVotesB++;
                    userIdAlreadyVotedList.Add(e.ChatMessage.UserId);
                    currentVotesTotal++;
                    OnVoteReceived?.Invoke();
                }
            } 

            messagesOfCurrentRound.Add(new TwitchMessageEntry(e.ChatMessage));
        }
    }

    #endregion

}
