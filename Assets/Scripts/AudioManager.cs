using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    private UnityAction endVotingListener;

    void Awake() {
        endVotingListener = new UnityAction(UpdateSound);
    }

    private void UpdateSound()
    {
        
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
