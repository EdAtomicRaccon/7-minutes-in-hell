using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ResolutionState : MonoBehaviour,IState
{
    public GameObject resolutionScreenPrefab;
    private GameObject resolutionScreenCanvas;
    void Awake()
    {
        resolutionScreenCanvas = Instantiate(Globals.Instance.resolutionCanvas);
        Button playButton = resolutionScreenCanvas.GetComponentInChildren<Button>();
        playButton.onClick.AddListener(on_click_play_button);
    }

    void on_click_play_button()
    {
        Debug.Log("This is the end, my friend");
        Destroy(resolutionScreenCanvas);
        //FindObjectOfType<GameManager>()._machine.Fire(Trigger.GAME_START);
    }
}
