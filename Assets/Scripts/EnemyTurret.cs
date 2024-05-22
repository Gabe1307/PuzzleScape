using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyTurret : MonoBehaviour

{
    public Transform player;
    public Transform laserPoint;
    public LineRenderer laserLine;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public AudioSource playSound;

    public float rotationSpeed = 5f;
    public float maxDistance = 10f;
    public LayerMask playerLayer;
    public float bulletInterval = 3f; // Time interval between bullet shots

    private bool playerInRange = false;
    private bool isShooting = false;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (laserLine == null)
        {
            laserLine = GetComponent<LineRenderer>();
        }
    }

    private void Update()
    {
        if (player == null || laserLine == null)
            return;

        // Rotate the turret to face the player
        Vector3 direction = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Check if the player is within the turret's detection range
        playerInRange = Vector3.Distance(transform.position, player.position) <= maxDistance;

        // Fire the laser at the player if in range
        if (playerInRange)
        {
            ShootLaser();
            if (!playSound.isPlaying)
            {
                playSound.Play();

            }
            
        }
        else
        {
            // If the player is out of range, disable the laser
            laserLine.enabled = false;
            // Stop shooting if player is out of range
            StopShooting();
            playSound.Stop();
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

            // Start shooting if not already shooting
            if (!isShooting)
            {
                InvokeRepeating("ShootBullet", 0f, bulletInterval);
                isShooting = true;
            }
        }
        else
        {
            // If the laser doesn't hit anything, set the endpoint to a point far away
            laserLine.SetPosition(1, laserPoint.position + direction * 100f);
            // Stop shooting if the player is out of range or the laser doesn't hit the player
            StopShooting();
        }
    }

    void StopShooting()
    {
        if (isShooting)
        {
            CancelInvoke("ShootBullet");
            isShooting = false;
        }
    }

    void ShootBullet()
    {
        
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}

