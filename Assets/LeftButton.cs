using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

using UnityEngine.UI;
using DG.Tweening;
public class LeftButton : MonoBehaviour
{
    private UnityAction leftClickListener;
    private UnityAction rightClickListener;
    private Image background;
    void Awake()
    {
        leftClickListener = new UnityAction(TriggerIn);
        rightClickListener = new UnityAction(TriggerOff);
        background = GetComponent<Image>();
    }


    void OnEnable()
    {
        EventManager.StartListening("LeftClick", TriggerIn);
        EventManager.StartListening("RightClick", TriggerOff);
    }

    void OnDisable()
    {
        EventManager.StopListening("LeftClick", TriggerIn);
        EventManager.StopListening("RightClick", TriggerOff);
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
