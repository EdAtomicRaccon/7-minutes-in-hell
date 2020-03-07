using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Stateless;

public enum Trigger
{
    PLAYER_ANSWER,
    CONTEXT_SET,
    TIMER_END,
    GAME_START,
    GAME_END,
    AUTHENTICATED,
    TRANSITION_END
};
public enum State
{
    TWITCH = 0,
    TITLE = 1,
    CONTEXT = 2,
    QUESTION = 3,
    RESOLUTION = 4,
    TRANSITION = 5
};

interface IState {
}

public class GameManager : MonoBehaviour
{
    public StateMachine<State, Trigger> _machine;
    private State currentState = State.TWITCH;
    
    private IState currentComponentState;

    public int step = 0;

    void Awake() {
        Debug.Log("Heho");
        InitStateMachine();
    }

    private void InitStateMachine()
    {
        Debug.Log("State machine configuration");

        _machine = new StateMachine<State, Trigger>(() => currentState, s => currentState = s);

        _machine.Configure(State.TWITCH)
            //.PermitReentry(Trigger.GRANULE_SELECTED)
            .Permit(Trigger.AUTHENTICATED, State.TITLE);

        _machine.Configure(State.TITLE)
            //.PermitReentry(Trigger.GRANULE_SELECTED)
            .OnEntry(t => LoadState<TitleState>())
            .Permit(Trigger.GAME_START, State.CONTEXT);

        _machine.Configure(State.CONTEXT)
            .OnEntry(t => LoadState<ContextState>())
            .Permit(Trigger.CONTEXT_SET, State.QUESTION);

        _machine.Configure(State.QUESTION)
            .OnEntry(t => LoadState<QuestionState>())
            .Permit(Trigger.TIMER_END, State.TRANSITION)
            .Permit(Trigger.PLAYER_ANSWER, State.TRANSITION);

        _machine.Configure(State.TRANSITION)
            .OnEntry(t=> LoadState<TransitionState>())
            .Permit(Trigger.TRANSITION_END,State.CONTEXT)
            .Permit(Trigger.GAME_END, State.RESOLUTION);

        _machine.Configure(State.RESOLUTION)
            .OnEntry(t => LoadState<ResolutionState>());

        //TODO : Move the fire
        _machine.Fire(Trigger.AUTHENTICATED);
    }

    private void LoadState<T>() {
        if(null != currentComponentState)
            Destroy(currentComponentState as Component);

        Debug.Log("Entering new State : " + typeof(T).ToString());

        Component c = gameObject.AddComponent(typeof(T));
        currentComponentState = c as IState;
    }
}
