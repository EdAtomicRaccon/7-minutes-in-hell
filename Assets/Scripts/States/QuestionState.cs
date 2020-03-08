using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionState : MonoBehaviour,IState
{
    private int countdown = 10;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading questions");
        Debug.Log("Start recording to Twitch chat");
        TwitchGameLogic.Instance.StartVoting();
        Debug.Log("Are you okay?");
        Debug.Log("A- Yes");
        Debug.Log("Z- No");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Globals.Instance.yourChoices.Add(0);
            Debug.Log("You answered yes");
            EventManager.TriggerEvent("LeftClick");
            PlayerAnswered();
        }
        if (Input.GetMouseButtonDown(0)) { 
            Globals.Instance.yourChoices.Add(1);
            EventManager.TriggerEvent("RightClick");
            PlayerAnswered();
        }

    }

    private void PlayerAnswered()
    {
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.PLAYER_ANSWER);
        EventManager.TriggerEvent("Answer");
        Globals.Instance.currentStep += 1;
    }
}
