using UnityEngine;
using System.Collections.Generic; // Required for List
using TMPro; // Required for TextMeshProUGUI
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
// list of defeated enemy IDs
    [SerializeField] private List<int> defeatedEnemyIDs = new List<int>();

    [Tooltip("Reference to a Text UI element to display defeated enemy count.")]
    [SerializeField] private TextMeshProUGUI defeatedCountText; // Requires TextMeshPro

    void Awake()
    {
        // Ensure the list is sorted at the start, though it should stay sorted through AddDefeatedEnemy.
        // This is a defensive measure.
        defeatedEnemyIDs.Sort();
        UpdateUI();
    }

    void OnEnable()
    {
        // Subscribe to defeat events from all currently active enemies.
        // This is important if enemies might exist before this script is enabled.
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            enemy.OnEnemyDefeated += AddDefeatedEnemy;
        }
    }

    void OnDisable()
    {
        // Unsubscribe from defeat events to prevent memory leaks.
        // It's good practice to unsubscribe from events you've subscribed to.
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            enemy.OnEnemyDefeated -= AddDefeatedEnemy;
        }
    }

    /// Adds a defeated enemy's ID to the sorted list, ensuring no duplicates.
    /// This method inserts the new ID in the correct position to maintain the sorted order.
    /// <param name="enemyID">The ID of the defeated enemy.</param>
    public void AddDefeatedEnemy(int enemyID)
    {
        // First, check if the enemyID is already in the list using binary search.
        if (!HasDefeatedEnemy(enemyID))
        {
            // If not present, find the correct insertion point to maintain sorted order.
            int insertIndex = 0;
            // Iterate until we find an ID greater than the new ID, or reach the end of the list.
            while (insertIndex < defeatedEnemyIDs.Count && defeatedEnemyIDs[insertIndex] < enemyID)
            {
                insertIndex++;
            }
            // Insert the new ID at the found index.
            defeatedEnemyIDs.Insert(insertIndex, enemyID);
            Debug.Log($"Added unique defeated enemy ID: {enemyID}. Total unique defeated: {defeatedEnemyIDs.Count}");
            UpdateUI();
        }
        else
        {
            // no duplicates allowed
        }
    }
    /// Checks if a specific enemy ID has been defeated using a binary search algorithm.
    /// Requires the 'defeatedEnemyIDs' list to be sorted for correctness.

    public bool HasDefeatedEnemy(int enemyID)
    {
        int low = 0;
        int high = defeatedEnemyIDs.Count - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2; // Prevents overflow compared to (low + high) / 2

            if (defeatedEnemyIDs[mid] == enemyID)
            {
                return true; // Found the enemy ID
            }
            else if (defeatedEnemyIDs[mid] < enemyID)
            {
                low = mid + 1; // Search in the right half
            }
            else
            {
                high = mid - 1; // Search in the left half
            }
        }
        return false; // Enemy ID not found
    }

    /// <summary>
    /// Returns the total count of unique enemies defeated.
    /// </summary>
    public int GetDefeatedEnemyCount()
    {
        return defeatedEnemyIDs.Count;
    }

    /// <summary>
    /// Updates the UI text element to display the current defeated enemy count.
    /// </summary>
    private void UpdateUI()
    {
        if (defeatedCountText != null)
        {
            defeatedCountText.text = defeatedCountText.ToString();
        }
    }
}

