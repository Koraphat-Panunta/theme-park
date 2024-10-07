using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;         // Movement speed of the player
    public float rotationSpeed = 100f;   // Rotation speed of the player
    public float jumpForce = 5f;         // Jump force applied to the player
    public float groundCheckDistance = 1.1f; // Distance for ground checking
    public LayerMask groundMask;         // Layers that define what is ground

    private Rigidbody rb;
    private bool isGrounded;
    public Transform weaponSocket;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();      // Handle player movement and rotation
        GroundCheck();         // Check if the player is on the ground

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();            // Handle jump when spacebar is pressed
        }
    }

    // Handle movement and rotation input
    private void HandleMovement()
    {
        // Forward and backward movement (W/S)
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Rotate left and right (A/D)
        float rotationY = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        // Apply movement
        transform.Translate(Vector3.forward * moveZ);

        // Apply rotation
        transform.Rotate(Vector3.up * rotationY);
    }

    // Apply jump force
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Check if the player is on the ground
    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
    }
}
