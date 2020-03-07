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
<<<<<<< HEAD

    public QuestionList questionList;

    public List<CommunityChoiceData> choices;

    void Awake() {
        Init();
    }

    private void Init() {
        choices = new List<CommunityChoiceData>();
    }
=======
>>>>>>> 0fad7daa31a3e8b9a90db6e35d305d9912bc017b
}
