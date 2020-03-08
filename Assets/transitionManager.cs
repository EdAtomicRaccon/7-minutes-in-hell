﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class transitionManager : MonoBehaviour
{
    private UnityAction propListener;
    public GameObject lightTransition;
    public GameObject cdCase;

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
        if (lightTransition.activeInHierarchy == false) lightTransition.SetActive(true);
        if (lightTransition.activeInHierarchy == true) lightTransition.SetActive(false);
        if (cdCase.activeInHierarchy == false) lightTransition.SetActive(true);
        if (cdCase.activeInHierarchy == true) lightTransition.SetActive(false);
    }
}
