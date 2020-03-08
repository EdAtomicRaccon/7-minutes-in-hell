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

    /// <summary>
    /// Calculates the current player-viewer-choice overlap
    /// </summary>
    /// <returns>The normalized opinion data in percent</returns>
    public float GetTotalOpinion()
    {
        float opinion = 0;

        if (choices == null)
        {
            Debug.LogError($"No community choice data.");
            return -1;
        }

        if (yourChoices == null)
        {
            Debug.LogError($"No player choice data.");
            return -1;
        }

        if (choices.Count != yourChoices.Count)
        {
            Debug.LogError($"Data mismatch. YourChoices: {yourChoices.Count}, CommunityChoices: {choices.Count}");
            return -1;
        }

        for (int i = 0; i < choices.Count; i++)
        {
            opinion += GetOpinion(i);
        }

        return opinion / choices.Count;
    }

    public float GetLatestOpinion()
    {
        return GetOpinion(yourChoices.Count - 1);
    }

    public float GetOpinion(int roundIndex)
    {
        return (yourChoices[roundIndex] < 0.5f) ? choices[roundIndex].ChoiceA : choices[roundIndex].ChoiceB;
    }
}
