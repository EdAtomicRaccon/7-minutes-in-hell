using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventString : UnityEvent<string> { }

[RequireComponent(typeof(InputField))]
public class InputFieldSubmit : MonoBehaviour
{
    InputField inputField;

    public UnityEventString OnSubmit;

    private void Awake()
    {
        inputField = GetComponent<InputField>();
    }

    private void Update()
    {
        if (inputField.isFocused && Input.GetButtonDown("Submit"))
		{
			if (!string.IsNullOrEmpty(inputField.text))
			{
                OnSubmit?.Invoke(inputField.text);
			}
        }
    }

    public void Submit()
    {
        OnSubmit?.Invoke(inputField.text);
    }
}
