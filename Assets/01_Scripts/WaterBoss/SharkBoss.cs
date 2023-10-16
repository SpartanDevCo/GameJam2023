using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBoss : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 5.0f;
    private int currentWaypoint = 0;
    private Transform player; // Reference to the player GameObject.

    public float stopAndShootDistance = 5.0f;
    private float shootingDuration = 5.0f;
    private float shootingTimer = 0.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10.0f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0.0f;
    
    private Quaternion originalRotation;
    private Quaternion targetRotation;

    private enum SharkBossState
    {
        Moving,
        Shooting,
    }
    
    private SharkBossState currentState = SharkBossState.Moving;

    private void Start()
    {
        player = GameObject.Find("Player").transform; // Replace "Player" with your player's GameObject name.
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        switch (currentState)
        {
            case SharkBossState.Moving:
                MoveToWaypoint();
                if (Vector3.Distance(transform.position, player.position) <= stopAndShootDistance)
                {
                    currentState = SharkBossState.Shooting;
                }
                break;

            case SharkBossState.Shooting:
                if (Vector3.Distance(transform.position, player.position) > stopAndShootDistance)
                {
                    currentState = SharkBossState.Moving;
                }
                else
                {
                    RotateTowardsPlayer();
                    ShootAtPlayer();
                }
                break;
        }
    }

    private void MoveToWaypoint()
    {
        if (currentWaypoint < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[currentWaypoint].position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0; // Loop back to the first waypoint
                }
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }

    private void ShootAtPlayer()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;

            // Adjust the bullet's properties, such as damage, particle effects, etc.

            Destroy(bullet, 2.0f); // Adjust the time as needed.
        }
        
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= shootingDuration)
        {
            currentState = SharkBossState.Moving;
            shootingTimer = 0.0f;
            transform.rotation = originalRotation; // Return to the original rotation when done shooting.
        }
    }
}
