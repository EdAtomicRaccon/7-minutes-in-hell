using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropAnimation : MonoBehaviour
{
    private UnityAction propListener;
    public string propName;
    public string[] animationMethodList;

    void Awake() {
        propListener = new UnityAction(TriggerAnimation);
    }


    void OnEnable()
    {
        EventManager.StartListening(propName, TriggerAnimation);
    }

    void OnDisable() { 
        EventManager.StopListening(propName, TriggerAnimation);
    }

    private void TriggerAnimation()
    {
        Invoke(animationMethodList[Random.Range(0, animationMethodList.Length - 1)], 0f);
    }
}
