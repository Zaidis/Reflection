using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_2 : TriggerEvent
{
    public GameObject box;
    public GameObject[] itemsToTurnOn;
    public GameObject[] signs;
    public GameObject theDoor;
    public Material newMaterial;
    public override void StartEvent() {
        print("Trent says hi!");
        box.SetActive(false);
        theDoor.GetComponent<Door>().OpenDoor();
        foreach(GameObject sign in signs) {
            sign.GetComponent<Renderer>().material = newMaterial;
        }

        foreach(GameObject item in itemsToTurnOn) {
            item.SetActive(true);
        }
    }

}
