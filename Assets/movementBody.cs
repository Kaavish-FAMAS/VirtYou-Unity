using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementBody : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalInput.ToString());
        float verticalInput = Input.GetAxis("Vertical");
        //Debug.Log(verticalInput.ToString());

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        rb.velocity.Normalize();

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}