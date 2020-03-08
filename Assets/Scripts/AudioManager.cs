using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    private UnityAction endVotingListener;

    public FMODUnity.StudioEventEmitter[] studioEventEmitters;

    void Awake() {
        endVotingListener = new UnityAction(UpdateSound);
    }

    private void UpdateSound()
    {
        float opinion = Globals.Instance.GetCurrentOpinion();

        foreach (var emitter in studioEventEmitters)
        {
            // set intensity
            emitter.SetParameter("Intensity", Globals.Instance.currentStep);

            // set user choice overlap
            emitter.SetParameter("opinion", opinion);
        }
    }

    void OnEnable()
    {
        EventManager.StartListening("EndVoting",endVotingListener);
        
    }

    void OnDisable()
    {
        EventManager.StopListening("EndVoting", endVotingListener);
        
    }
}
