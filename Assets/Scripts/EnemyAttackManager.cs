using UnityEngine;
using System.Collections.Generic; // Required for List
using System.Linq; // Required for .ToList()

/// <summary>
/// Manages enemy attacks based on player proximity.
/// It uses a Bubble Sort algorithm to identify the closest enemy
/// and instructs it to attack if conditions are met.
/// </summary>
public class EnemyProximityAttackManager : MonoBehaviour
{
    [Tooltip("Drag the Player GameObject's Transform here.")]
    public Transform playerTransform;

    [Tooltip("Reference to the PlayerHealth component on the player.")]
    public PlayerHealth playerHealth; // To apply damage

    [Tooltip("How often (in seconds) the system should check for closest enemy and trigger attacks.")]
    public float attackCheckInterval = 0.5f; // Check every 0.5 seconds

    // List to hold all 'EnemyAttackAI' components found in the scene.
    // This list will be sorted by proximity.
    private List<EnemyAttackAI> _activeEnemies = new List<EnemyAttackAI>();

    private float _timer; // Internal timer for attack check interval

    void Start()
    {
        // --- Input Validation ---
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned! Enemy Attack Manager cannot function.");
            enabled = false;
            return;
        }
        if (playerHealth == null)
        {
            Debug.LogError("Player Health component is not assigned! Enemy Attack Manager cannot function.");
            enabled = false;
            return;
        }

        // Find all EnemyAttackAI components in the scene at Start.
        _activeEnemies = FindObjectsOfType<EnemyAttackAI>().ToList();

        Debug.Log("--- Initial Enemy List ---");
        PrintEnemyList();

        // Perform initial check and sort
        CheckAndTriggerAttacks();
        Debug.Log("\n--- Initial Attack Check Complete ---");
        PrintEnemyList(); // Print sorted list

        _timer = attackCheckInterval; // Initialize timer
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            CheckAndTriggerAttacks();
            // Debug.Log($"\n--- Attack Check & Enemies Re-Sorted at {Time.timeSinceLevelLoad:F2}s ---");
            // PrintEnemyList(); // Can be too verbose, enable for deep debug
            _timer = attackCheckInterval; // Reset timer
        }
    }

    /// <summary>
    /// Calculates the proximity for all active enemies, sorts them by proximity
    /// using Bubble Sort, and then triggers an attack from the closest eligible enemy.
    /// </summary>
    public void CheckAndTriggerAttacks()
    {
        if (playerTransform == null || playerHealth == null) return;

        // Clean up list by removing any null references (e.g., if enemies were destroyed)
        _activeEnemies.RemoveAll(item => item == null);

        // Step 1: Update the DistanceToPlayer for each enemy.
        // This MUST happen BEFORE sorting, as distances change with player/enemy movement.
        Vector3 currentPlayerPosition = playerTransform.position;
        foreach (EnemyAttackAI enemy in _activeEnemies)
        {
            if (enemy != null)
            {
                enemy.DistanceToPlayer = Vector3.Distance(currentPlayerPosition, enemy.transform.position);
            }
        }

        // Step 2: Perform Bubble Sort on the list based on DistanceToPlayer.
        // THIS IS THE BUBBLE SORT ALGORITHM IMPLEMENTATION:
        int n = _activeEnemies.Count;
        bool swapped; // Flag to check if any swaps occurred in a pass

        for (int i = 0; i < n - 1; i++) // Outer loop for passes (n-1 passes needed)
        {
            swapped = false; // Assume no swaps in this pass
            for (int j = 0; j < n - i - 1; j++) // Inner loop for comparisons
            {
                // Compare adjacent enemies based on their DistanceToPlayer
                // We want to sort in ASCENDING order (closest first).
                // If the current element (activeEnemies[j]) is GREATER than
                // the next element (activeEnemies[j + 1]), then they are in the wrong order
                // and need to be swapped.
                if (_activeEnemies[j].DistanceToPlayer > _activeEnemies[j + 1].DistanceToPlayer)
                {
                    // SWAP the two elements
                    EnemyAttackAI tmp = _activeEnemies[j];
                    _activeEnemies[j] = _activeEnemies[j + 1];
                    _activeEnemies[j + 1] = tmp;
                    swapped = true; // Mark that a swap occurred in this pass
                }
            }

            // Optimization: If no two elements were swapped by the inner loop
            // during a complete pass, it means the list is already sorted,
            // so we can stop early to save unnecessary comparisons.
            if (!swapped)
            {
                break;
            }
        }

        // Step 3: Iterate through the now sorted list and trigger an attack from the first eligible enemy.
        // We only tell the CLOSEST one that's ready and in range to attack.
        foreach (EnemyAttackAI enemy in _activeEnemies)
        {
            if (enemy != null && enemy.IsPlayerInAttackRange() && enemy.IsAttackReady())
            {
                enemy.Attack(playerHealth); // Tell the enemy to attack
                return; // Only the closest eligible enemy attacks per check interval
            }
        }
    }

    /// <summary>
    /// Helper method to print the current state of the enemy list to the console.
    /// </summary>
    void PrintEnemyList()
    {
        if (_activeEnemies.Count == 0)
        {
            // no active enemies
            return;
        }
        foreach (EnemyAttackAI enemy in _activeEnemies)
        {
            if (enemy != null)
            {
                Debug.Log(enemy.ToString());
            }
        }
    }
}