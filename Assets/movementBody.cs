using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementBody : MonoBehaviour
{
    Rigidbody rb;
    public Transform vrCamera;
    public float movementSpeed = 6f;

    /*
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        vrCamera = transform.parent;
        //initialCameraOffset = transform.localPosition;
    }

    private void FixedUpdate()
    {
        // Get movement input from input device
        float horizontalMovement = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalMovement);
        float verticalMovement = Input.GetAxis("Vertical");
        //Debug.Log(verticalMovement);

        // Calculate movement vector based on input
        Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement) * movementSpeed;
        // Apply movement to rigidbody velocity
        rb.velocity = movement;
        rb.velocity.Normalize();
        // Move camera based on cube position
        //vrCamera.localPosition = transform.localPosition + initialCameraOffset;
        vrCamera.position = transform.localPosition;
        //Debug.Log();
    }
    */

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //vrCamera = Camera.main.transform;
    }

    void FixedUpdate()
    {
        // Get the camera's forward direction without any vertical component
        Vector3 cameraForward = Vector3.ProjectOnPlane(vrCamera.forward, Vector3.up).normalized;

        // Calculate movement based on input and camera direction
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //Debug.Log(verticalInput);
        //Debug.Log(horizontalInput);
        Vector3 movement = (horizontalInput * vrCamera.right + verticalInput * cameraForward).normalized * movementSpeed;

        // Apply movement to the rigidbody
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }
}

