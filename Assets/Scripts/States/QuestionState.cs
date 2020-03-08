﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionState : MonoBehaviour,IState
{
    private int countdown = 5;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading questions");
        Debug.Log("Start recording to Twitch chat");
        TwitchGameLogic.Instance.StartVoting();
        Debug.Log("Are you okay?");
        Debug.Log("A- Yes");
        Debug.Log("Z- No");

        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Globals.Instance.yourChoices.Add(0);
            Debug.Log("You answered yes");
            PlayerAnswered();
        }
        if (Input.GetKeyDown(KeyCode.Z)) { 
            Globals.Instance.yourChoices.Add(1);
            Debug.Log("You answered no");
            PlayerAnswered();
        }

    }

    IEnumerator CountDown() {
        for (int i = 0; i < countdown; i++)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Time left" + (countdown - i).ToString());
        }
        Debug.Log("Time over");
        Globals.Instance.yourChoices.Add(0);
        PlayerAnswered();
    }

    private void PlayerAnswered() { 
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.PLAYER_ANSWER);
        EventManager.TriggerEvent("Answer");
        Globals.Instance.currentStep += 1;
    }
}
