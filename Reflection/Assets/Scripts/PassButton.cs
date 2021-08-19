using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassButton : Interactable
{
    public int num; //what number it holds
    public override void StartInteraction() {
        this.transform.parent.GetComponent<Pass>().AddNumber(num);
    }

}
