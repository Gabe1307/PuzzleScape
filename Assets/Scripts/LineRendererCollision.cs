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
        if (lineRenderer != null && lineRenderer.positionCount > 1)
        {
            // Check if the line renderer hits the object to show
            RaycastHit hit;
            Vector3 startPos = lineRenderer.GetPosition(0);
            Vector3 endPos = lineRenderer.GetPosition(1);

            if (Physics.Linecast(startPos, endPos, out hit))
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
        else
        {
            // Hide the object if the line renderer doesn't have enough positions
            objectToShow.SetActive(false);
        }
    }
}

