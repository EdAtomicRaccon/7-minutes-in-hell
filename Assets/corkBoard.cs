using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class corkBoard : MonoBehaviour
{
    private UnityAction propListener;
    public List<GameObject> corkboardObjects;

    void Awake()
    {
        propListener = new UnityAction(TriggerAnimation);
    }


    void OnEnable()
    {
        EventManager.StartListening("cork", TriggerAnimation);
    }

    void OnDisable()
    {
        EventManager.StopListening("cork", TriggerAnimation);
    }

    private void TriggerAnimation()
    {
        if (corkboardObjects[0].activeInHierarchy)
        {
            corkboardObjects[0].SetActive(false);
            corkboardObjects[1].SetActive(true);
        }
        if (corkboardObjects[1].activeInHierarchy)
        {
            corkboardObjects[1].SetActive(false);
            corkboardObjects[2].SetActive(true);
        }

        if (corkboardObjects[2].activeInHierarchy)
        {
            corkboardObjects[2].SetActive(false);
            corkboardObjects[3].SetActive(true);
        }
        if (corkboardObjects[3].activeInHierarchy)
        {
            corkboardObjects[3].SetActive(false);
            corkboardObjects[4].SetActive(true);
        }
    }
}
