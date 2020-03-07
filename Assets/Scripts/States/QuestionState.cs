using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            EventManager.TriggerEvent("Answer");
            Debug.Log("You answered yes");
            PlayerAnswered();
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            EventManager.TriggerEvent("Answer");
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
        PlayerAnswered();
    }

    private void PlayerAnswered() {
        Globals.Instance.currentStep += 1;
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.PLAYER_ANSWER);
    }
}
