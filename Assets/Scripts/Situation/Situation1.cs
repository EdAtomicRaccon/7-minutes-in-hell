using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Situation1 : MonoBehaviour, ISituation
{
    void OnEnable()
    {
        Play();
    }
    public void Play()
    {
        Debug.Log("Playing situation 1...");
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {

        EventManager.TriggerEvent("cork");
        EventManager.TriggerEvent("postIt");
        EventManager.TriggerEvent("poster");
        EventManager.TriggerEvent("transition");

        yield return new WaitForSeconds(3f);
        EnviroFunctions.Instance.MailPop();
        yield return null;
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.CONTEXT_SET);
    }
}
