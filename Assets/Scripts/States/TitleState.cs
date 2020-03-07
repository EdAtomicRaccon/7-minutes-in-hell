using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleState : MonoBehaviour, IState
{
    public GameObject titleScreenPrefab;
    private GameObject titleScreenCanvas;
    void Awake() {
        titleScreenCanvas = Instantiate(Globals.Instance.titleCanvas);
        Button playButton = titleScreenCanvas.GetComponentInChildren<Button>();
        playButton.onClick.AddListener(on_click_play_button);
    }

    void on_click_play_button() {
        Globals.Instance.canMove = true;
        Destroy(titleScreenCanvas);
        FindObjectOfType<GameManager>()._machine.Fire(Trigger.GAME_START);
    }
}
