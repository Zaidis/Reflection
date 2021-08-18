using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetKey : MonoBehaviour
{

    public Transform[] possibleLocations;
    public Transform myLocation;
    private void Start() {
        int rand = Random.Range(0, possibleLocations.Length);

        this.transform.position = possibleLocations[rand].position;
        myLocation = possibleLocations[rand];
    }

}
