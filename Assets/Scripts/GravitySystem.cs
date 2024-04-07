using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GravitySystem : MonoBehaviour
{
    public float gravity = -9.81f; // Gravitational acceleration value

    private CharacterController characterController; // Reference to CharacterController component if attached
    private Rigidbody rigidbody; // Reference to Rigidbody component if attached
    private bool useCharacterController = false; // Flag to indicate if CharacterController is used

    private void Start()
    {
        // Check if CharacterController is attached
        characterController = GetComponent<CharacterController>();
        if (characterController != null)
        {
            useCharacterController = true;
        }
        else
        {
            // Check if Rigidbody is attached
            rigidbody = GetComponent<Rigidbody>();
            if (rigidbody == null)
            {
                // If neither CharacterController nor Rigidbody is attached, log a warning
                Debug.LogWarning("GravitySystem: No CharacterController or Rigidbody found. Gravity will not be applied.");
            }
        }
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (useCharacterController)
        {
            // If using CharacterController, apply gravity directly to it
            if (!characterController.isGrounded)
            {
                characterController.Move(Vector3.up * gravity * Time.fixedDeltaTime);
            }
        }
        else if (rigidbody != null)
        {
            // If using Rigidbody, apply gravity using Rigidbody's AddForce method
            rigidbody.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
        }
    }
}
