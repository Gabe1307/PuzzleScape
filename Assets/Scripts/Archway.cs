using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Archway : MonoBehaviour
{
    
   

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
           
            
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +3);
        }
    }
}
