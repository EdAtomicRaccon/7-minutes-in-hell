using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MugAnimation : PropAnimation
{
    void Awake() {
        propName = "mug";
        animationMethodList = new string[] {"Hover","ChangeColor"};
    }

    private void Hover() {
        Vector3 currentPosition = transform.position;
        Vector3 currentRotation = transform.rotation.eulerAngles;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(currentPosition.y + 0.2f, 2f))
            .Join(transform.DOLocalRotate(new Vector3(0f,currentRotation.y + 360f,0f), 4f))
            .Append(transform.DOLocalMoveY(currentPosition.y, 2f));
        
        Debug.Log("Hovering...");
    }

    private void ChangeColor() {
        Debug.Log("Changing color...");
    }
}
