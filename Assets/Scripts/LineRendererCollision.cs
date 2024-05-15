using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineRendererCollision : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject objectToShow;
    public GameObject objectToHit;

    private void Start()
    {
        // Ensure the object to show is initially hidden
        objectToShow.SetActive(false);
    }

    private void Update()
    {
        // Check if the line renderer hits the object to show
        RaycastHit hit;
        if (Physics.Linecast(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1), out hit))
        {
            if (hit.collider.gameObject == objectToHit)
            {
                // Show the object when hit
                objectToShow.SetActive(true);
            }
            else
            {
                // Hide the object if it's not hit
                objectToShow.SetActive(false);
            }
        }
        else
        {
            // Hide the object if the line renderer doesn't hit anything
            objectToShow.SetActive(false);
        }
    }
}

