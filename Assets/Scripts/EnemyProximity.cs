using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyManager : MonoBehaviour
{

    public PlayerAttack attack;
    [SerializeField] private Transform playerTransform;

    // The maximum distance from the player for an enemy to start following.")]
    [SerializeField] private float followDistanceThreshold = 10f;

// "How often (in seconds) the enemies should be sorted and their following status updated.
    [SerializeField] private float sortUpdateInterval = 0.5f;

    // A list to hold all the Enemy scripts in the scene.
    private List<Enemy> allEnemies = new List<Enemy>();
    private float currentSortTimer;

    // Nested class to hold an Enemy and its calculated distance for sorting.
    private class EnemyProximity
    {
        public Enemy enemy;
        public float distance;
    }

    void Start()
    {
        // Find all active Enemy components in the scene.
        allEnemies = FindObjectsOfType<Enemy>().ToList();

        // Initialize each enemy with a reference to the player.
        foreach (Enemy enemy in allEnemies)
        {
            enemy.playerTransform = playerTransform;
        }

        currentSortTimer = sortUpdateInterval; // Set initial timer to trigger sort on first update
    }

    void Update()
    {
        // Decrement the timer.
        currentSortTimer -= Time.deltaTime;

        // If the timer reaches zero or less, it's time to sort and update.
        if (currentSortTimer <= 0f)
        {
            SortAndManageEnemies();
            currentSortTimer = sortUpdateInterval; // Reset the timer
        }
    }

    private void SortAndManageEnemies()
    {
        if (playerTransform == null)
        {
            return;
        }

        // Create a list of EnemyProximity objects to store enemies and their distances.
        List<EnemyProximity> enemiesWithDistances = new List<EnemyProximity>();
        foreach (Enemy enemy in allEnemies)
        {
            if (enemy != null) // Ensure the enemy GameObject hasn't been destroyed
            {
                float dist = Vector3.Distance(playerTransform.position, enemy.transform.position);
                enemiesWithDistances.Add(new EnemyProximity { enemy = enemy, distance = dist });
            }
        }

// bubble sort
        int n = enemiesWithDistances.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                // If the current element's distance is greater than the next element's distance, swap them.
                if (enemiesWithDistances[j].distance > enemiesWithDistances[j + 1].distance)
                {
                    // Perform the swap.
                    EnemyProximity temp = enemiesWithDistances[j];
                    enemiesWithDistances[j] = enemiesWithDistances[j + 1];
                    enemiesWithDistances[j + 1] = temp;
                }
            }
        }
        // bubble sort end

        // if (attack != null) attack.target = enemiesWithDistances[0].enemy.transform;

        // Now that the list is sorted by distance (closest first), update enemy following statu
        foreach (EnemyProximity ep in enemiesWithDistances)
        {
            if (ep.enemy != null) // Double check enemy is not null after sorting
            {
                // If the enemy is within the threshold, make it follow.
                if (ep.distance <= followDistanceThreshold)
                {
                    ep.enemy.SetFollowing(true);
                }
                else
                {
                    // If the enemy is outside the threshold, make it stop following.
                    ep.enemy.SetFollowing(false);
                }
            }
        }


    }
}
