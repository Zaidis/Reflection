using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Lever : Interactable
{
    public bool triggerEvent;
    public PlayableDirector dir;
    public bool isUsed;
    private void Awake() {
        dir = GetComponent<PlayableDirector>();
    }
    public override void StartInteraction() {
        if (!isUsed) {
            dir.Play();
            isUsed = true;
            this.GetComponent<TriggerEvent>().StartEvent();
        }     

    }

}
