using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_1 : TriggerEvent
{
    //This event will spawn a box in the second room and play a sound effect
    public GameObject box;
    public GameObject easterEgg;
    public AudioClip sound;
    public override void StartEvent() {
        box.SetActive(true);
        easterEgg.SetActive(true);
        GameManager.instance.PlaySound(sound);
        Destroy(this.gameObject);
    }

}
