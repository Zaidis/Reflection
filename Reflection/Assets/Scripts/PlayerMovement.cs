using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Teleporter
{
    [SerializeField] private float speed; //player speed
    [SerializeField] private float gravity = -9.81f; //player gravity amount
    [SerializeField] private Transform origin;
    [SerializeField] private float jumpHeight = 3f;
    public bool canMove = true;
    Vector3 velocity;
    public CharacterController controller;
    private float groundDistance = 0.4f;
    [SerializeField] bool isGrounded;
    public LayerMask groundMask;
    public LayerMask interactable;
    private void Awake() {
        controller = GetComponent<CharacterController>();
    }
    
    private void Update() {
        if (canMove) {
            isGrounded = Physics.CheckSphere(origin.position, groundDistance, groundMask);

            if (isGrounded && velocity.y <= 0) {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * Time.deltaTime * speed);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
                //jump
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, 1.5f, interactable)) {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            if (hit.collider.gameObject.layer == 7) {
                //interactable layer is seven
                CamMovement.instance.crosshair.color = Color.red;
            } else {
                CamMovement.instance.crosshair.color = Color.white;
            }
        } else {
            CamMovement.instance.crosshair.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            //interact button
            if(Physics.Raycast(ray, out hit, 1.5f, interactable)) {
                if (hit.collider.gameObject.CompareTag("Door")) {
                    hit.collider.gameObject.GetComponent<Door>().StartInteraction();
                } else if (hit.collider.gameObject.CompareTag("Flashlight")) {
                    Destroy(hit.collider.gameObject);
                    CamMovement.instance.hasFlashlight = true;
                    guides.instance.GiveGuide();
                } else if (hit.collider.gameObject.CompareTag("Key")) {
                    hit.collider.gameObject.GetComponent<Key>().StartInteraction();
                }
            }
        }

        
    }

    public override void Teleport(Transform oldPortal, Transform newPortal, Vector3 pos, Quaternion rot) {
        transform.position = pos;
        transform.rotation = rot;
        Vector3 newRotation = rot.eulerAngles;

        velocity = newPortal.TransformVector(oldPortal.InverseTransformVector(velocity));
        Physics.SyncTransforms();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Door")) {
            if(guides.instance.i == 1)
                guides.instance.GiveGuide();

            if (other.GetComponent<Door>().isLocked) {
                int neededKey = other.GetComponent<Door>().keyId;

                if(!GameManager.instance.CheckKey(neededKey))
                    guides.instance.ChangeText(other.GetComponent<Door>().lockedLine);

            }
        } else if(other.CompareTag("Flashlight")) {
            if(guides.instance.i == 2) {
                guides.instance.GiveGuide();
            }
        } else if (other.CompareTag("Key")) {

        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Door")) {
            if (other.GetComponent<Door>().isLocked) {
                guides.instance.ChangeText("");
            }
        }
    }
}
