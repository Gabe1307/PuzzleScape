using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}


public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 2.0f; // Set a default interaction range
    private Rotatable selectedLaser;
    private IInteractable interactObj;
    private bool isInteracting;

    void Start()
    {
        selectedLaser = null;
        interactObj = null;
        isInteracting = false;
    }

    void Update()
    {
        // Check for start interaction
        if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            StartInteraction();
        }

        // Check for stop interaction
        if (Input.GetKeyUp(KeyCode.E) && isInteracting)
        {
            StopInteracting();
        }
    }

    void FixedUpdate()
    {
        // Check for out of range condition during interaction
        if (isInteracting && selectedLaser != null)
        {
            if (Vector3.Distance(InteractorSource.position, selectedLaser.transform.position) > InteractRange)
            {
                StopInteracting();
            }
        }
    }

    void StartInteraction()
    {
        // Try to start interaction if not currently interacting
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out interactObj))
            {
                selectedLaser = hitInfo.collider.gameObject.GetComponent<Rotatable>();
                if (selectedLaser != null)
                {
                    interactObj.Interact();
                    selectedLaser.Interacted();
                    isInteracting = true;
                    Debug.Log("Started interacting with: " + hitInfo.collider.gameObject.name);
                }
            }
        }
    }

    void StopInteracting()
    {
        if (selectedLaser != null)
        {
            selectedLaser.Interacted();
            Debug.Log("Stopped interacting with: " + selectedLaser.gameObject.name);
            selectedLaser = null;
        }
        interactObj = null;
        isInteracting = false;
    }
}

