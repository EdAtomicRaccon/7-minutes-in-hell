using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSubmitInputField : MonoBehaviour
{
    Button button;

    public InputFieldSubmit inputField;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Submit);
    }

    void Submit()
    {
        inputField?.Submit();
    }
}
