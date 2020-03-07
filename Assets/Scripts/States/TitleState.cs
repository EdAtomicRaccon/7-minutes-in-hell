using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleState : MonoBehaviour, IState
{
    public TitleState() { 
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
            FindObjectOfType<GameManager>()._machine.Fire(Trigger.GAME_START);
    }
}
