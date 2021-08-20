using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Door : Interactable
{
    public Collider triggerCollider;
    public PlayableDirector dir;
    public bool opened;
    [Header("If car is locked")]
    public bool isLocked;
    public string lockedLine;
    public int keyId; //what key it needs to open
    private void Awake() {
        dir = this.GetComponent<PlayableDirector>();
    }
    public override void StartInteraction() {
        //print("I am interacting!");
        if (!opened) {
            if (!isLocked) {
                OpenDoor();
            } else {
                //it is locked
                if (GameManager.instance.CheckKey(keyId)) {
                    isLocked = false;
                    OpenDoor();
                }
                    
            }
        }
    }

    public override void StopInteraction() {
        Destroy(triggerCollider);
    }

    public void OpenDoor() {
        dir.Play();
        opened = true;
        GetComponent<AudioSource>().Play();
        StopInteraction();
    }

}
