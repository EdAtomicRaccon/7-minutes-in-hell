using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ButtonLoadNextScene : MonoBehaviour
{
    Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(LoadNextScene);
    }

    void LoadNextScene()
    {
        int targetIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (targetIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Can't load next scene. End of scene list!");
        } else
        {
            SceneManager.LoadScene(targetIndex);
        }
    }
}
