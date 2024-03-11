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
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interacting == false)
            {
                // if we are not interacting with an object we then start interacting with it.
                Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
                if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
                {
                    if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    {
                        // enables the bool to rotate the selected object.
                        interactObj.Interact();
                        selectedLaser = hitInfo.collider.gameObject.GetComponent<Rotatable>();
                        selectedLaser.Interacted();



                    }

                }


            }
            else
            {
                // if we are interacting and we have pressed E the bool in rotatable is set back to false. 
                // we also set the refrence to null so when u press e it doesnt have the refrence anymore.
                selectedLaser.Interacted();
                selectedLaser = null;
            }   
            
            

        }
    }
}
