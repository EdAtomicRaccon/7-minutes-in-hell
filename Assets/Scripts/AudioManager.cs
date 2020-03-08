using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    private UnityAction endVotingListener;
    private UnityAction playListener;

    private FMODUnity.StudioEventEmitter[] studioEventEmitters;
    public FMODUnity.StudioEventEmitter[] playStudioEventEmitter;

    void Awake() {
        endVotingListener = new UnityAction(UpdateSound);
        playListener = new UnityAction(PlaySound);
        studioEventEmitters = GetComponentsInChildren<FMODUnity.StudioEventEmitter>();
    }

    private void PlaySound()
    {
        //playStudioEventEmitter.
    }

    private void UpdateSound()
    {
        float opinion = Globals.Instance.GetTotalOpinion();

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
        EventManager.StartListening("PlayButton", playListener);
        
    }

    void OnDisable()
    {
        EventManager.StopListening("EndVoting", endVotingListener);
        EventManager.StopListening("PlayButton", playListener);
    }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.Button]
#endif
    public void SetParametersForAll(string paramName, float paramValue)
    {
        foreach (var emitter in studioEventEmitters)
        {
            emitter.SetParameter(paramName, paramValue);
        }
    }
}
