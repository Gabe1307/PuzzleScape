using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healAmount = 20;  // Amount of health this item restores

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure to tag your player GameObject with "Player"
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject);  // Destroy the item after pickup
            }
        }
    }
}