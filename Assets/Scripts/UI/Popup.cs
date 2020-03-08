using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Question;
    [SerializeField]
    private TextMeshProUGUI Answer1;
    [SerializeField]
    private TextMeshProUGUI Answer2;

    private UnityAction answeredListener;

    public RectTransform loadingBar;

    private float loadingBarSize;

    public int countdown = 10;
    void Awake() {
        loadingBarSize = loadingBar.sizeDelta.y;
        answeredListener = new UnityAction(HidePopup);
    }
    void OnEnable() {
        EventManager.StartListening("Answer", answeredListener);
        ShowPopup();
        SetText();
        StartCoroutine(CountDown());
    }

    void OnDisable() {
        EventManager.StopListening("Answer", answeredListener);
        StopAllCoroutines();
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
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.4f).Append(transform.DOLocalMove(new Vector3(0f,-400f, 0f),1f).OnComplete(DestroyPopup));
    }

    private void DestroyPopup() {
        Destroy(gameObject);
    }

    IEnumerator CountDown()
    {
        for (int i = 0; i < countdown; i++)
        {
            yield return new WaitForSeconds(1f);
            loadingBar.DOScaleX((1 - (float)i / (float)countdown), 1f).SetEase(Ease.Linear);
            Debug.Log("Time left" + (countdown - i).ToString());
        }
        Debug.Log("Time over");
        Globals.Instance.yourChoices.Add(Globals.Instance.choiceTmp);
        PlayerAnswered();
    }

    private void PlayerAnswered()
    {
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.PLAYER_ANSWER);
        EventManager.TriggerEvent("Answer");
        Globals.Instance.currentStep += 1;
    }
}
