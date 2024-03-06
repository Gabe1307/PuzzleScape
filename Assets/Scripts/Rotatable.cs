using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotatable : MonoBehaviour
{

    [SerializeField] private InputAction pressed, axis;

    [SerializeField] private float speed = 1;
    private bool inverted;

    private Transform cam;
    private Vector2 rotation;

    private bool rotateAllowed;
    private void Awake()
    {
        cam = Camera.main.transform;
        pressed.Enable();
        axis.Enable();
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    //coroutine to rotate 
    private IEnumerator Rotate()
    {
        rotateAllowed = true;
        while (rotateAllowed)
        {
            //apply rotattion 
            rotation *= speed;
            transform.Rotate(Vector3.up, rotation.x, Space.World);
            transform.Rotate(-cam.right * (inverted ? -1: 1), rotation.y, Space.World) ;
            yield return null;
        }
    }
}
