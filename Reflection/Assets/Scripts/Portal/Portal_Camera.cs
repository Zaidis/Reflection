using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Camera : MonoBehaviour
{
    public List<Portal> portals = new List<Portal>();

    private void Awake() {
        Portal[] temp = FindObjectsOfType<Portal>();
        foreach(Portal p in temp) {
            portals.Add(p);
        }
    }
    
    private void OnPreCull() {
        for(int i = 0; i < portals.Count; i++) {
            portals[i].Render();
        }
        for (int i = 0; i < portals.Count; i++) {
            portals[i].PostRender();
        }
    }
}
