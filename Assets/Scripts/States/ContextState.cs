using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextState : MonoBehaviour,IState
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading the context");
        StartCoroutine(EventList());
    }

    IEnumerator EventList() {
        Debug.Log("Context : Some event happens");
        yield return new WaitForSeconds(1f);
        Debug.Log("Context : Another event");
        yield return new WaitForSeconds(1f);
        Debug.Log("Context : Again another event");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.CONTEXT_SET);
    }
}
