using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : Singleton<Globals>
{
    public int currentStep = 0;
    public bool canMove = false;

    public GameObject titleCanvas;
    public GameObject computerCanvas;
    public GameObject mailPopup;
}
