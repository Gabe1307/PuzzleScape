using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Archway : MonoBehaviour
{
    
    public string Level1;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            
            SceneManager.LoadScene(Level1);
        }
    }
}
