using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserReflector : MonoBehaviour
{
    // Variables for handling mouse input
    private Vector3 screenPoint;
    private Vector3 offset;

    // Variables for storing laser position and direction
    Vector3 position;
    Vector3 direction;

    // LineRenderer component for drawing the laser
    LineRenderer lr;

    // Flag indicating if the reflector is currently open
    public bool isOpen;

    // Reference to the reflector that the laser has bounced off from
    GameObject tempReflector;

    void Start()
    {
        // Initialize variables and components
        isOpen = false;
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isOpen)
        {
            // If the reflector is open, activate the LineRenderer
            lr.positionCount = 2;
            lr.SetPosition(0, position);

            // Cast a ray in the direction of reflection
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, Mathf.Infinity))
            {
                // If the ray hits another reflector, recursively open its ray
                if (hit.collider.CompareTag("Reflector"))
                {
                    tempReflector = hit.collider.gameObject;
                    Vector3 temp = Vector3.Reflect(direction, hit.normal);
                    hit.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hit.point, temp);
                }
                // Set the endpoint of the LineRenderer to the hit point
                lr.SetPosition(1, hit.point);
            }
            else
            {
                // If the ray doesn't hit anything, set the endpoint to a point 
                if (tempReflector)
                {
                    tempReflector.GetComponent<LaserReflector>().CloseRay();
                    tempReflector = null;
                }
                lr.SetPosition(1, direction * 0);
            }
        }
        else
        {
            // If the reflector is closed, deactivate the LineRenderer
            CloseRay();
        }
    }

    // Open the reflector's ray
    public void OpenRay(Vector3 pos, Vector3 dir)
    {
        isOpen = true;
        position = pos;
        direction = dir;
    }

    // Close the reflector's ray
    public void CloseRay()
    {
        isOpen = false;
        lr.positionCount = 0; // Hide the LineRenderer
    }
}




