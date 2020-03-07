using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISituation {
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
        EnviroFunctions.Instance.MailPop();
        yield return null;
    }
}

