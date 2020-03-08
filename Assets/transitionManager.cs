using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class transitionManager : MonoBehaviour
{
    private UnityAction propListener;
    public Light lightTransition;
    public List<GameObject> disapear;
    private float maxIntensity = 63;
    private float intensityAmount = 5;
    private float minIntensity = 1;

    void Awake()
    {
        propListener = new UnityAction(TriggerAnimation);
    }


    void OnEnable()
    {
        lightTransition = lightTransition.GetComponent<Light>();
        EventManager.StartListening("transition", TriggerAnimation);
    }

    void OnDisable()
    {
        EventManager.StopListening("transiton", TriggerAnimation);
    }

    private void TriggerAnimation()
    {
        while (lightTransition.intensity < maxIntensity)
        {
            lightTransition.intensity += intensityAmount * Time.deltaTime;
        }
    }
}
