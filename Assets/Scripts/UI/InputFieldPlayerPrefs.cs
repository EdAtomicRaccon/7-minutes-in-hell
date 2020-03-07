using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldPlayerPrefs : MonoBehaviour
{
    InputField inputField;

    public string playerPrefsKey;

    private void Awake()
    {
        inputField = GetComponent<InputField>();

        inputField.text = PlayerPrefs.GetString(playerPrefsKey, string.Empty);
    }

}
