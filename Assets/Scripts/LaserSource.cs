using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define a class LaserSource that inherits from MonoBehaviour
public class LaserSource : MonoBehaviour
{
    [SerializeField] Transform laserStartPoint; // Start point of the laser
    Vector3 direction; // Direction in which the laser will be emitted
    LineRenderer lr; // Component to render the laser line
    GameObject tempReflector; // Temporary holder for the last reflector hit by the laser
    public LayerMask reflectLayer; // Layer mask to specify which layers the laser can interact with

    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>(); // Get the LineRenderer component
        direction = laserStartPoint.forward; // Set the initial laser direction to the forward direction of the start point
        lr.positionCount = 2; // Set the number of positions to be rendered by the LineRenderer
        lr.SetPosition(0, laserStartPoint.position); // Set the starting position of the laser line
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; // Variable to store information about what the laser hits
        // Cast a ray from the laser start point in the specified direction, checking for collisions with the reflectLayer
        if (Physics.Raycast(laserStartPoint.position, direction, out hit, Mathf.Infinity, reflectLayer))
        {
            // Check if the hit object has the tag "Reflector"
            if (hit.collider.CompareTag("Reflector"))
            {
                tempReflector = hit.collider.gameObject; // Store the reflector object
                Vector3 temp = Vector3.Reflect(direction, hit.normal); // Calculate the reflection direction based on the hit normal
                // Call the OpenRay method on the hit reflector component with the hit point and reflection direction
                hit.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hit.point, temp);
            }
            lr.SetPosition(1, hit.point); // Set the end position of the laser line to the hit point
        }
        else
        {
            // If the laser does not hit a reflector
            if (tempReflector)
            {
                tempReflector.GetComponent<LaserReflector>().CloseRay(); // Close the ray on the reflector
                tempReflector = null; // Clear the temporary reflector
            }
            lr.SetPosition(1, direction * 200); // Set the laser to extend far off in the set direction if nothing is hit
        }
    }
}

