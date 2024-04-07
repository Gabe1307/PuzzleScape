using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Example: Check if the player's health is zero and perform game over actions
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Decrease player's health by specified amount
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Ensure health doesn't go below zero
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    // Increase player's health by specified amount
    public void Heal(int amount)
    {
        currentHealth += amount;

        // Ensure health doesn't exceed maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    // Example: Perform actions when player dies
    void Die()
    {
        // Add your game over logic here, such as restarting the level or showing a game over screen.
        Debug.Log("Player died.");
    }
}
