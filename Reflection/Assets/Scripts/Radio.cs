using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{

    public override void StartInteraction() {
        this.GetComponent<TriggerEvent>().StartEvent();
    }

}
