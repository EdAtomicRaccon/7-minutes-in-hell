using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Summary : MonoBehaviour
{
    private TextMeshProUGUI summaryText;
    void OnEnable()
    {
        summaryText = GetComponent<TextMeshProUGUI>();
        summaryText.text = GetStats();
    }

    private string GetStats()
    {
        string summary = "";
        List<CommunityChoiceData> choices = Globals.Instance.choices;
        List<int> yourChoices = Globals.Instance.yourChoices;

        summary += (choices[0].ChoiceA * 100f).ToString() + " % of the viewers chose to save the doctor.\n";
        summary += (choices[0].ChoiceB * 100f).ToString() + " % of the viewers chose to save the architect.\n";
        summary += "You chose to save the " + (yourChoices[0] == 0 ? "architect" : "doctor") + ".\n\n";

        summary += (choices[1].ChoiceA * 100f).ToString() + " % of the viewers chose to save their friend.\n";
        summary += (choices[1].ChoiceB * 100f).ToString() + " % of the viewers chose to save the innocent man.\n";
        summary += "You chose to save the " + (yourChoices[1] == 0 ? "your friend" : "the innocent man") + ".\n\n";
        
        summary += (choices[2].ChoiceA * 100f).ToString() + " % of the viewers chose to stay put.\n";
        summary += (choices[2].ChoiceB * 100f).ToString() + " % of the viewers chose to push the workers away.\n";
        summary += "You chose to " + (yourChoices[2] == 0 ? "stay put" : "push them away") + ".\n\n";
        
        summary += (choices[3].ChoiceA * 100f).ToString() + " % of the viewers chose to tell their mom.\n";
        summary += (choices[3].ChoiceB * 100f).ToString() + " % of the viewers chose to hide the truth\n";
        summary += "You chose to " + (yourChoices[3] == 0 ? "tell her" : "hide the truth") + ".\n\n";

        summary += (choices[4].ChoiceA * 100f).ToString() + " % of the viewers chose to cover the entrance.\n";
        summary += (choices[4].ChoiceB * 100f).ToString() + " % of the viewers chose not to get implicated.\n";
        summary += "You chose to " + (yourChoices[4] == 0 ? "cover the entrance" : "stay away") + ".\n\n";

        summary += (choices[5].ChoiceA * 100f).ToString() + " % of the viewers chose to denounce their brother.\n";
        summary += (choices[5].ChoiceB * 100f).ToString() + " % of the viewers chose to stay silent.\n";
        summary += "You chose to " + (yourChoices[5] == 0 ? "denounce him" : "stay silent") + ".\n\n";

        summary += (choices[6].ChoiceA * 100f).ToString() + " % of the viewers chose to go with 7 refugees.\n";
        summary += (choices[6].ChoiceB * 100f).ToString() + " % of the viewers chose to fill the boat.\n";
        summary += "You chose to save the " + (yourChoices[6] == 0 ? "go with 7 people" : "fill the boat") + ".\n";

        return summary;
    }
}
