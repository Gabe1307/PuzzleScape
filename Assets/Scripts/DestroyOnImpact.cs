using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DestroyOnImpact : MonoBehaviour
{
    // Define the object to be destroyed when hit
    public GameObject objectToDestroy;

    // Define what triggers the destruction
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with this one has a specific tag
        if (other.CompareTag("Bullet"))
        {
            // Destroy the object assigned to objectToDestroy
            Destroy(objectToDestroy);
        }
    }
}
