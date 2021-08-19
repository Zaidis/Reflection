using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{

    public int id;
    public string keyName;
    public bool triggersEvent; //if picking this up will trigger an event or not
    public override void StartInteraction() {
        GameManager.instance.AddKey(id);
        if (triggersEvent) {
            this.GetComponent<TriggerEvent>().StartEvent();
            guides.instance.ChangeText("");
            return;
        }
        guides.instance.ChangeText("");
        Destroy(this.gameObject);
    }
}
