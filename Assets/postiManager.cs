using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class postiManager : MonoBehaviour
{
    private UnityAction propListener;
    public List<GameObject> postItText;

    void Awake()
    {
        propListener = new UnityAction(TriggerAnimation);
    }


    void OnEnable()
    {
        EventManager.StartListening("postIt", TriggerAnimation);
    }

    void OnDisable()
    {
        EventManager.StopListening("postIt", TriggerAnimation);
    }
    private void TriggerAnimation()
    {
        if (postItText[0].activeInHierarchy)
        {
            postItText[0].SetActive(false);
            postItText[1].SetActive(true);
        }
        else if (postItText[1].activeInHierarchy)
        {
            postItText[1].SetActive(false);
            postItText[2].SetActive(true);
        }

        else if (postItText[2].activeInHierarchy)
        {
            postItText[2].SetActive(false);
            postItText[3].SetActive(true);
        }
        else if (postItText[3].activeInHierarchy)
        {
            postItText[3].SetActive(false);
            postItText[4].SetActive(true);
        }
        else if (postItText[4].activeInHierarchy)
        {
            postItText[4].SetActive(false);
            postItText[5].SetActive(true);
        }
    }
}
