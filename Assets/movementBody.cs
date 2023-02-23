using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementBody : MonoBehaviour
{
    Rigidbody rb;
    public Transform vrCamera;
    public float movementSpeed = 6f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        vrCamera = Camera.main.transform;
    }

    void FixedUpdate()
    {
        // Get the camera's forward direction without any vertical component
        Vector3 cameraForward = Vector3.ProjectOnPlane(vrCamera.forward, Vector3.up).normalized;

        // Calculate movement based on input and camera direction
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = (horizontalInput * vrCamera.right + verticalInput * cameraForward).normalized * movementSpeed;

        // Apply movement to the rigidbody
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }
}