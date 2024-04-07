using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public Transform player;
    public Transform laserPoint;
    public LineRenderer laserLine;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public float rotationSpeed = 5f;
    public float maxDistance = 10f;
    public LayerMask playerLayer;
    public float bulletInterval = 3f; // Time interval between bullet shots

    private bool playerInRange = false;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        // Rotate the turret to face the player
        Vector3 direction = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Check if the player is within the turret's detection range
        if (Vector3.Distance(transform.position, player.position) <= maxDistance)
        {
            playerInRange = true;
            // Fire the laser at the player
            ShootLaser();
        }
        else
        {
            playerInRange = false;
            // If the player is out of range, disable the laser
            laserLine.enabled = false;
        }
    }

    void ShootLaser()
    {
        // Enable the laser line renderer
        laserLine.enabled = true;

        // Set the starting point of the laser at the laserPoint position
        laserLine.SetPosition(0, laserPoint.position);

        // Calculate direction from turret to player
        Vector3 direction = player.position - laserPoint.position;

        RaycastHit hit;

        // Check if the laser hits something
        if (Physics.Raycast(laserPoint.position, direction, out hit, Mathf.Infinity, playerLayer))
        {
            // If the laser hits the player, set the endpoint of the laser to the hit point
            laserLine.SetPosition(1, hit.point);
            // Here you can add code to deal damage to the player or apply other effects

            // Check if it's time to shoot a bullet
            if (!IsInvoking("ShootBullet") && playerInRange)
            {
                InvokeRepeating("ShootBullet", 0f, bulletInterval);
            }
        }
        else
        {
            // If the laser doesn't hit anything, set the endpoint to a point far away
            laserLine.SetPosition(1, laserPoint.position + direction * 100f);
            // Cancel the bullet shooting if the player is out of range or the laser doesn't hit the player
            if (IsInvoking("ShootBullet"))
            {
                CancelInvoke("ShootBullet");
            }
        }
    }

    void ShootBullet()
    {
        // Instantiate a bullet prefab at the bullet spawn point
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
