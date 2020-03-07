using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranstionState : MonoBehaviour,IState
{
    // Start is called before the first frame update
    void Start()
    {
        CommunityChoiceData communityCD = TwitchGameLogic.Instance.EndVoting();
        Globals.Instance.choices.Add(communityCD);

        if(Globals.Instance.currentStep > 6)
            FindObjectOfType<GameManager>()._machine.Fire(Trigger.GAME_END);
        else
            FindObjectOfType<GameManager>()._machine.Fire(Trigger.TRANSITION_END);
    }
}
