using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CamMovement : MonoBehaviour
{

    public float sensitivity = 100f;
    public Transform body;
    float xRotation = 0f;
    float x, y;

    bool gameStarted;
    public Image crosshair;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        //after 38 seconds (after the boom sound) we move the camera to the player
        Invoke("TransitionCamera", 38f);
    }
    private void Update() {
        if (gameStarted) {
            x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            xRotation -= y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            body.Rotate(Vector3.up * x);
        }

    }
    public void TransitionCamera() {
        this.GetComponent<Animator>().enabled = false;
        this.transform.parent = body;
        this.transform.localPosition = Vector3.zero;
        this.transform.localPosition = new Vector3(0, 0.838f, 0); //good placement for the player cam ---> 0.233 == z
        crosshair.gameObject.SetActive(true);
        gameStarted = true;
    }
}