using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PicturesManager : MonoBehaviour
{
    private UnityAction propListener;
    public List<GameObject> poster;

    void Awake()
    {
        propListener = new UnityAction(TriggerAnimation);
    }


    void OnEnable()
    {
        EventManager.StartListening("poster", TriggerAnimation);
    }

    void OnDisable()
    {
        EventManager.StopListening("poster", TriggerAnimation);
    }
    private void TriggerAnimation()
    {
        if (poster[0].activeInHierarchy)
        {
            poster[0].SetActive(false);
            poster[1].SetActive(true);
        }
        else if (poster[1].activeInHierarchy)
        {
            poster[1].SetActive(false);
            poster[2].SetActive(true);
        }

        else if (poster[2].activeInHierarchy)
        {
            poster[2].SetActive(false);
            poster[3].SetActive(true);
        }
        else if (poster[3].activeInHierarchy)
        {
            poster[3].SetActive(false);
            poster[4].SetActive(true);
        }
        else if (poster[4].activeInHierarchy)
        {
            poster[4].SetActive(false);
            poster[5].SetActive(true);
        }
        else if (poster[5].activeInHierarchy)
        {
            poster[5].SetActive(false);
            poster[6].SetActive(true);
        }
        else if (poster[6].activeInHierarchy)
        {
            poster[6].SetActive(false);
            poster[7].SetActive(true);
        }
    }
}
