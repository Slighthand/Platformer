using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] public int enemyID;

   // set by EnemyManager
    public Transform playerTransform; // Public so EnemyManager can set it

    private bool isFollowing = false;
    public event Action<int> OnEnemyDefeated;

    Rigidbody2D physics;

    void Start() {
        physics = GetComponent<Rigidbody2D>();
    }

    // tell the enemy whether to follow or stop following
    public void SetFollowing(bool follow)
    {
        isFollowing = follow;
    }

    public void Die()
    {
        // Invoke the event, passing this enemy's ID.
        OnEnemyDefeated?.Invoke(enemyID);
        Debug.Log($"Enemy ID {enemyID} defeated!");
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Die();

        }
    }

    void Update()
    {
        // If the enemy is set to follow and the player transform is assigned, move towards the player.
        if (isFollowing && playerTransform != null)
        {
            // Calculate the direction to the player.
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Move the enemy towards the player.
            
            physics.velocity = directionToPlayer * followSpeed;
            // transform.position += directionToPlayer * followSpeed * Time.deltaTime;
        }
    }
}
