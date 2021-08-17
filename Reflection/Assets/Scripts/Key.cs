using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{

    public int id;
    public string keyName;

    public override void StartInteraction() {
        GameManager.instance.AddKey(id);
        Destroy(this.gameObject);
    }
}
