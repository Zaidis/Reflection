using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_2 : TriggerEvent
{
    public GameObject box;
    public GameObject[] signs;
    public Material newMaterial;
    public override void StartEvent() {
        print("Trent says hi!");
        box.SetActive(false);

        foreach(GameObject sign in signs) {
            sign.GetComponent<Renderer>().material = newMaterial;
        }
    }

}
