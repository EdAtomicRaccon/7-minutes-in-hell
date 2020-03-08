using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionState : MonoBehaviour, IState
{
    void Start()
    {
        CommunityChoiceData ccd = TwitchGameLogic.Instance.EndVoting();

        Globals.Instance.choices.Add(ccd);
        EventManager.TriggerEvent("EndVoting");
        EventManager.TriggerEvent("transition");


        if(Globals.Instance.currentStep>6)
            FindObjectOfType<GameManager>()._machine.Fire(Trigger.GAME_END);
        else
            FindObjectOfType<GameManager>()._machine.Fire(Trigger.TRANSITION_END);
    }

}
