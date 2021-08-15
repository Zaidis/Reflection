using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 previousOffset { get; set; }
    public virtual void Teleport(Transform oldPortal, Transform newPortal, Vector3 pos, Quaternion rot) {
        transform.position = pos;
        transform.rotation = rot;
    }

    public virtual void EnterPortal() {

    }

    public virtual void ExitPortal() {

    }
}
