using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;
using DG.Tweening;

public class RightButton : MonoBehaviour
{
    private UnityAction rightClickListener;
    private Image background;
    void Awake()
    {
        rightClickListener = new UnityAction(TriggerAnimation);
        background = GetComponent<Image>();
    }


    void OnEnable()
    {
        EventManager.StartListening("RightClick", TriggerAnimation);
    }

    void OnDisable()
    {
        EventManager.StopListening("RightClick", TriggerAnimation);
    }

    private void TriggerAnimation()
    {
        background.color = Color.red;
    }
}
