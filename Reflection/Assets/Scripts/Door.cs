using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Door : Interactable
{
    public float openSpeed; //how fast the door opens
    public Collider triggerCollider;
    public PlayableDirector dir;
    public bool opened;
    private void Awake() {
        dir = this.GetComponent<PlayableDirector>();
    }
    public override void StartInteraction() {
        //print("I am interacting!");
        if (!opened) {
            dir.Play();
            opened = true;
            StopInteraction();
        }
    }

    public override void StopInteraction() {
        Destroy(triggerCollider);
    }

    

}
