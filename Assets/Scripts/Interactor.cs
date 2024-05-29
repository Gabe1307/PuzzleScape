using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}


public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    private bool Interacting;
    private Rotatable selectedLaser;

    void Update()
    {
        // Check if the E key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If not already interacting, start interacting with an object within range
            if (!Interacting)
            {
                Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
                {
                    if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    {
                        // Start interacting with the object
                        Interacting = true;
                        interactObj.Interact();
                        selectedLaser = hitInfo.collider.gameObject.GetComponent<Rotatable>();
                        selectedLaser.Interacted();
                    }
                }
            }
            else
            {
                // If already interacting, stop interacting
                selectedLaser.Interacted();
                selectedLaser = null;
                Interacting = false;
            }
        }

        // Check for WASD keys to stop interaction
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (Interacting)
            {
                selectedLaser.Interacted();
                selectedLaser = null;
                Interacting = false;
            }
        }
    }
}







