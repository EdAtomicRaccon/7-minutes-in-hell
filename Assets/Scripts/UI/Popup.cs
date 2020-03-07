using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
public class Popup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Question;
    [SerializeField]
    private TextMeshProUGUI Answer1;
    [SerializeField]
    private TextMeshProUGUI Answer2;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InOutBounce);
        SetText();
    }

    private void SetText()
    {
        Question.text = Globals.Instance.questionList.questions[Globals.Instance.currentStep].question;
        Answer1.text = Globals.Instance.questionList.questions[Globals.Instance.currentStep].answer1;
        Answer2.text = Globals.Instance.questionList.questions[Globals.Instance.currentStep].answer2;
    }
}
