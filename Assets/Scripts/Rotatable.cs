using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Rotatable : MonoBehaviour, IInteractable
{
    [SerializeField] private InputAction pressed, axis;
    [SerializeField] private float speed = 1;
    private bool rotateAllowed;
    private Vector2 rotation;

    public void Interact()
    {
        Debug.Log(Random.Range(0, 100));
    }

    public void Interacted()
    {
        rotateAllowed = !rotateAllowed;
        Debug.Log("Interacted: rotateAllowed = " + rotateAllowed);
    }

    private void Awake()
    {
        pressed.Enable();
        axis.Enable();
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; Debug.Log("Pressed canceled: rotateAllowed = " + rotateAllowed); };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    // Coroutine to rotate
    private IEnumerator Rotate()
    {
        while (rotateAllowed)
        {
            // Apply rotation 
            rotation *= speed;
            transform.Rotate(Vector3.up, rotation.x, Space.World);
            transform.Rotate(transform.right, rotation.y, Space.World);
            yield return null;
        }
    }
}

