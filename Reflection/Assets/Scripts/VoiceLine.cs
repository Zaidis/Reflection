using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLine : MonoBehaviour
{

    public AudioSource source;
    public bool triggerEvent;
    private void Awake() {
        source = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            source.Play();
            if (triggerEvent) {
                this.GetComponent<TriggerEvent>().StartEvent();
            }
            Destroy(this.GetComponent<Collider>());
        }
    }

    public void PlaySound() {
        source.Play();
        Destroy(this.GetComponent<Collider>());
    }
}
