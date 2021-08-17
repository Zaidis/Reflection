using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TV : MonoBehaviour
{
    public GameObject theSound;
    private void Start() {
        Invoke("StartSound", 49.14f);
    }

    public void StartSound() {
        theSound.SetActive(true);
    }
}
