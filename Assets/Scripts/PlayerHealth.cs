using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class PlayerHealth : MonoBehaviour


{
    public int maxHealth = 100; // The maximum health of the player
    public int currentHealth; // The current health of the player
    public Image healthBar; // Reference to the UI Image for the health bar

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
        UpdateHealthBar(); // Initialize the health bar to the current health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by the damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure current health doesn't drop below 0
        UpdateHealthBar(); // Update the health bar to reflect the new health

        if (currentHealth <= 0)
        {
            Die(); // Handle player death
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount; // Increase current health by the heal amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure current health doesn't exceed max health
        UpdateHealthBar(); // Update the health bar to reflect the new health
    }

    private void UpdateHealthBar()
    {
        // Calculate the health percentage and update the health bar's fill amount
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        // Placeholder for handling player death (e.g., show game over screen)
        Debug.Log("Player is dead!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with a turret projectile
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Deal 20 damage to the player
            TakeDamage(20);

            // Destroy the turret projectile upon collision
            Destroy(collision.gameObject);
        }
    }
}



