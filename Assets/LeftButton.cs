using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

using UnityEngine.UI;
using DG.Tweening;
public class LeftButton : MonoBehaviour
{
    private UnityAction leftClickListener;
    private Image background;
    void Awake()
    {
        leftClickListener = new UnityAction(TriggerAnimation);
        background = GetComponent<Image>();
    }


    void OnEnable()
    {
        EventManager.StartListening("LeftClick", TriggerAnimation);
    }

    void OnDisable()
    {
        EventManager.StopListening("LeftClick", TriggerAnimation);
    }

    private void TriggerAnimation()
    {
        background.color = Color.red;
    }
}
