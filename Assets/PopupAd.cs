using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class PopupAd : MonoBehaviour
{
    public int popupNumber = 0;
    void OnEnable()
    {
        ShowPopup();
    }

    private void ShowPopup()
    {
        transform.DOLocalMove(new Vector3(Random.Range(-100f,100f), Random.Range(-100f, 100f)), 1f);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (popupNumber < 0)
                DestroyPopup();
            popupNumber -= 1;
        }
    }
    
    private void DestroyPopup()
    {
        Destroy(gameObject);
    }
}
