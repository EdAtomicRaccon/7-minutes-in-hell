using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : Singleton<Globals>
{
    public int currentStep = 0;
    public bool canMove = false;

    public GameObject titleCanvas;
    public GameObject computerCanvas;
    public GameObject mailPopup;
    public QuestionList questionList;

    public List<CommunityChoiceData> choices;

    void Awake() {
        Init();
    }

    private void Init() {
        choices = new List<CommunityChoiceData>();
    }

}
