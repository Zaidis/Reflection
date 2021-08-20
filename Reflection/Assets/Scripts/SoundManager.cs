using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        Invoke("PlayMusic", 48f);
    }
    public void PlayMusic() {
        GetComponent<AudioSource>().Play();
       // Debug.Log("I am playing music");
    }

    public void StopMusic() {
        GetComponent<AudioSource>().Stop();
    }
}
