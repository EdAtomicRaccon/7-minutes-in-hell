using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionState : MonoBehaviour, IState
{
    void Start()
    {
        CommunityChoiceData ccd = TwitchGameLogic.Instance.EndVoting();

        EventManager.TriggerEvent("EndVoting");
        EventManager.TriggerEvent("transition");

        Globals.Instance.choices.Add(ccd);

        if(Globals.Instance.currentStep>6)
            FindObjectOfType<GameManager>()._machine.Fire(Trigger.GAME_END);
        else
            FindObjectOfType<GameManager>()._machine.Fire(Trigger.TRANSITION_END);
    }

}
