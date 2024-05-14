using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    
    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //OnHealthChanged?.Invoke(currentHealth);

        Debug.Log("Healed: " + amount + " points.");
    }

    
    void Die()
    {
        
        Debug.Log("Player died.");
    }
}
