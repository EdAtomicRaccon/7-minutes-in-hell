using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Situation5 : MonoBehaviour
{
    void OnEnable()
    {
        Play();
    }
    public void Play()
    {
        Debug.Log("Playing situation 5...");
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(3f);
        EnviroFunctions.Instance.MailPop();
        yield return null;
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.CONTEXT_SET);
    }
}
