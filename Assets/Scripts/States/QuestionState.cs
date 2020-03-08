using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionState : MonoBehaviour,IState
{
    private int countdown = 5;
    private bool answered = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading questions");
        Debug.Log("Start recording to Twitch chat");
        TwitchGameLogic.Instance.StartVoting();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !answered) {
            Globals.Instance.yourChoices.Add(0);
            Debug.Log("You answered yes");
            EventManager.TriggerEvent("LeftClick");
            answered = true;
        }
        if (Input.GetMouseButtonDown(0) && !answered) { 
            Globals.Instance.yourChoices.Add(1);
            EventManager.TriggerEvent("RightClick");
            answered = true;
        }

    }
}
