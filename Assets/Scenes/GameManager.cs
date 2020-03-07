using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Stateless;
public enum Trigger
{
    PLAYER_ANSWER,
    TIMER_END,
    GAME_START,
    GAME_END
};
public enum State
{
    TITLE = 0,
    CONTEXT = 1,
    QUESTION = 2,
    RESOLUTION = 3
};

public class GameManager : MonoBehaviour
{
    public StateMachine<State, Trigger> _machine;
    private State currentState = State.TITLE;

    public int step = 0;

    void Awake() { 
        
    }

    private void InitStateMachine()
    {
        _machine = new StateMachine<State, Trigger>(() => currentState, s => currentState = s);

        _machine.Configure(State.TITLE)
            //.PermitReentry(Trigger.GRANULE_SELECTED)
            .OnEntry(t => new TitleState())
            .Permit(Trigger.GAME_START, State.QUESTION);
    }

    private void LoadState() {
    }

    // Start is called before the first frame update
    void Start()
    {
        //Load the context on screen

        //Wait for the context to be shown

        //Load the two possible answers

        //Start Listening Twitch integration

        //Manage the player input & stop listening Twitch integration -> public method in the

        //Go back to first step
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
