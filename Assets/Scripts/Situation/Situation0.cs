using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISituation {
    void Play();
}

public class Situation0 : MonoBehaviour , ISituation
{
    void OnEnable() {
        Play();
    }
    public void Play()
    {
        Debug.Log("Playing situation 0...");
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence() {
        
        yield return new WaitForSeconds(3f);
        EnviroFunctions.Instance.MailPop();
        yield return new WaitForSeconds(3f);
        EventManager.TriggerEvent("SomethinHappens");
        yield return null;
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.CONTEXT_SET);
    }
}

