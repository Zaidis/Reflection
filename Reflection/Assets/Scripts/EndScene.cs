using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{

    public void Start() {
        Invoke("QuitTheGame", 50f);
    }
    public void QuitTheGame() {
        Debug.Log("Quitting the game");
        Application.Quit();
    }
}
