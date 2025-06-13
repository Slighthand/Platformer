using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float followSpeed = 5f;

   // set by EnemyManager
    public Transform playerTransform; // Public so EnemyManager can set it

    private bool isFollowing = false;

    // tell the enemy whether to follow or stop following
    public void SetFollowing(bool follow)
    {
        isFollowing = follow;
    }

    void Update()
    {
        // If the enemy is set to follow and the player transform is assigned, move towards the player.
        if (isFollowing && playerTransform != null)
        {
            // Calculate the direction to the player.
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Move the enemy towards the player.
            transform.position += directionToPlayer * followSpeed * Time.deltaTime;
        }
    }
}
