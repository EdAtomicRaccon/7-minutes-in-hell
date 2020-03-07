using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextState : MonoBehaviour,IState
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading the context. Scenario is : " + Globals.Instance.currentStep);
        LoadSituation(Globals.Instance.currentStep);
    }

    private void LoadSituation(int currentStep)
    {
        switch (Globals.Instance.currentStep) {
            case 0:
                gameObject.AddComponent(typeof(Situation0));
                break;
            case 1:
                gameObject.AddComponent(typeof(Situation1));
                break;
            default:
                break;
        }
    }
}
