using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Question list", menuName = "SevenMinutes/QuestionList", order = 1)]
public class QuestionList : ScriptableObject
{
    [TableList]
    public Question[] questions;
}

[System.Serializable]
public class Question {
    public string question;
    public string answer1;
    public string answer2;
}
