using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class transitionManager : MonoBehaviour
{
    private UnityAction propListener;
    public GameObject lightTransition;

    void Awake()
    {
        propListener = new UnityAction(TransitionChanges);
    }


    void OnEnable()
    {
        EventManager.StartListening("transition", TransitionChanges);
    }

    void OnDisable()
    {
        EventManager.StopListening("transition", TransitionChanges);
    }

    private void TransitionChanges()
    {
        lightTransition.SetActive(true);
    }
}
