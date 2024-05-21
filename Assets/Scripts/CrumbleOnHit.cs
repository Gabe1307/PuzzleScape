using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class CrumbleOnHit : MonoBehaviour
{
    // The tag of the object that can cause this object to crumble
    public string triggeringTag = "Bullet";

    // The number of pieces the object will break into
    public int numberOfPieces = 10;

    // Prefab of a single piece
    public GameObject piecePrefab;

    // Called when the object collides with another collider
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the specific tag
        if (collision.collider.CompareTag(triggeringTag))
        {
            Crumble();
        }
    }

    // Method to handle the crumbling process
    private void Crumble()
    {
        // Get the original object's bounds
        Bounds bounds = GetComponent<Renderer>().bounds;

        // Break the object into pieces
        for (int i = 0; i < numberOfPieces; i++)
        {
            // Calculate a random position within the bounds of the object
            Vector3 randomPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );

            // Instantiate a piece at the random position
            Instantiate(piecePrefab, randomPosition, Quaternion.identity);
        }

        // Destroy the original object
        Destroy(gameObject);
    }
}



