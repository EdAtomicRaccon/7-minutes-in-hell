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
        Debug.Log("Start listening to Twitch chat");
        Debug.Log("Are you okay?");
        Debug.Log("A- Yes");
        Debug.Log("Z- No");
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { 
            Debug.Log("You answered yes");
            FindObjectOfType<GameManager>()._machine.Fire(Trigger.PLAYER_ANSWER);
        }
        if (Input.GetKeyDown(KeyCode.Z)) { 
            Debug.Log("You answered no");
            FindObjectOfType<GameManager>()._machine.Fire(Trigger.PLAYER_ANSWER);
        }

    }

    IEnumerator CountDown() {
        for (int i = 0; i < countdown; i++)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Time left" + (countdown - i).ToString());
        }
        Debug.Log("Time over");
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.PLAYER_ANSWER);
    }
}
