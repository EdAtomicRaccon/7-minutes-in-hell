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

    private void OnEnable()
    {
        if (Globals.Instance.choiceTmp == 0)
        {
            EventManager.TriggerEvent("LeftClick");
        } else if (Globals.Instance.choiceTmp == 1)
        {
            EventManager.TriggerEvent("RightClick");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Globals.Instance.choiceTmp = 0;
            EventManager.TriggerEvent("LeftClick");
        }
        if (Input.GetMouseButtonDown(0)) { 
            Globals.Instance.choiceTmp = 1;
            EventManager.TriggerEvent("RightClick");
        }

    }
}
