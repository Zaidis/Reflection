using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_4 : TriggerEvent
{
    //This event will spawn a box
    public GameObject box;

    //public AudioClip sound;
    public override void StartEvent() {
        box.SetActive(true);
        Destroy(this.GetComponent<Collider>());
        //GameManager.instance.PlaySound(sound);
        GetComponent<AudioSource>().Play();

        Invoke("StopEvent", 0.6f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartEvent();
        }
    }

    private void StopEvent() {
        box.SetActive(false);
        GetComponent<AudioSource>().Stop();
        Destroy(this.gameObject);
    }
}
