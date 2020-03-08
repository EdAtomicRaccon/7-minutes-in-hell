using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;
using DG.Tweening;

public class RightButton : MonoBehaviour
{
    private UnityAction leftClickListener;
    private UnityAction rightClickListener;
    private Image background;
    void Awake()
    {
        leftClickListener = new UnityAction(TriggerOff);
        rightClickListener = new UnityAction(TriggerIn);
        background = GetComponent<Image>();
    }


    void OnEnable()
    {
        EventManager.StartListening("LeftClick", TriggerOff);
        EventManager.StartListening("RightClick", TriggerIn);
    }

    void OnDisable()
    {
        EventManager.StopListening("LeftClick", TriggerOff);
        EventManager.StopListening("RightClick", TriggerIn);
    }

    private void TriggerIn()
    {
        background.color = Color.red;
    }

    private void TriggerOff()
    {
        background.color = Color.white;
    }
}
