using UnityEngine;
using System.Collections.Generic; // Required for List
using TMPro; // Required for TextMeshProUGUI
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
// list of defeated enemy IDs
    [SerializeField] private List<int> defeatedEnemyIDs = new List<int>();
    [SerializeField] public TextMeshProUGUI defeatedCountText;

    void Awake()
    {
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
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            enemy.OnEnemyDefeated -= AddDefeatedEnemy;
        }
    }

    /// Adds a defeated enemy's ID to the sorted list, ensures no duplicates.
    public void AddDefeatedEnemy(int enemyID)
    {
        // First, check if the enemyID is already in the list using binary search.
        if (!HasDefeatedEnemy(enemyID))
        {
            // If not present, find the correct insertion point to maintain sorted order.
            int index = 0;
            // Iterate until we find an ID greater than the new ID, or reach the end of the list.
            while (index < defeatedEnemyIDs.Count && defeatedEnemyIDs[index] < enemyID)
            {
                index++;
            }
            // Insert the new ID at the found index.
            defeatedEnemyIDs.Insert(index, enemyID);
            UpdateUI();
        }
        else
        {
            // no duplicates allowed
        }
    }
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
            defeatedCountText.text = $"Defeated: {GetDefeatedEnemyCount()}";
        }
    }
}

