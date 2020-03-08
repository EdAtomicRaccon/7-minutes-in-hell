using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : Singleton<Globals>
{
    public int currentStep = 0;
    public bool canMove = false;

    public GameObject titleCanvas;
    public GameObject computerCanvas;
    public GameObject resolutionCanvas;
    public GameObject mailPopup;
    public GameObject adPopup;
    public QuestionList questionList;

    public List<CommunityChoiceData> choices;
    public List<int> yourChoices;
    void Awake() {
        Init();
    }

    private void Init() {
        choices = new List<CommunityChoiceData>();
    }

}
