using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine.Events;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Question;
    [SerializeField]
    private TextMeshProUGUI Answer1;
    [SerializeField]
    private TextMeshProUGUI Answer2;

    private UnityAction answeredListener;

    void Awake() {
        answeredListener = new UnityAction(HidePopup);
    }
    void OnEnable() {
        EventManager.StartListening("Answer", answeredListener);
        ShowPopup();
        SetText();
    }

    void OnDisable() {
        EventManager.StopListening("Answer", answeredListener);
    }

    private void DebugStuff()
    {
        Debug.Log("Stuff");
    }

    private void ShowPopup() {
        transform.DOMoveY(0, 1f);
    }

    private void SetText()
    {
        Question.text = Globals.Instance.questionList.questions[Globals.Instance.currentStep].question;
        Answer1.text = Globals.Instance.questionList.questions[Globals.Instance.currentStep].answer1;
        Answer2.text = Globals.Instance.questionList.questions[Globals.Instance.currentStep].answer2;
        Question.ForceMeshUpdate();
        Answer1.ForceMeshUpdate();
        Answer2.ForceMeshUpdate();
    }

    private void HidePopup() { 
        transform.DOLocalMove(new Vector3(0f,-400f, 0f),1f).OnComplete(DestroyPopup);
    }

    private void DestroyPopup() {
        Destroy(gameObject);
    }
}
