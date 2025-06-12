using UnityEngine;
using System.Collections.Generic; // Required for List
using System.Linq; // Required for .ToList()

/// <summary>
/// Manages a list of coins and sorts them based on their proximity to the player
/// using the Bubble Sort algorithm. It then controls their visibility.
/// </summary>
public class CoinProximityManager : MonoBehaviour
{
 // "Drag the Player GameObject's Transform here.")]
    public Transform playerTransform;

// ("Coins within this distance from the player will be visible.")
    public float displayRange = 10.0f; // e.g., 10 units

// "How often (in seconds) the coin list should be re-sorted and visibility updated."
    public float updateInterval = 1.0f; // Update every 1 second

    // List to hold all 'CoinProximityTarget' components found in the scene.
    private List<CoinProximityTarget> _activeCoins = new List<CoinProximityTarget>();

    private float _timer; // Internal timer for update interval

    void Start()
    {
        // --- Input Validation ---
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned! Please assign your player GameObject to the 'Player Transform' slot in the Inspector.");
            enabled = false; // Disable this script if essential reference is missing
            return;
        }

        // Find all CoinProximityTarget components in the scene at Start.
        _activeCoins = FindObjectsOfType<CoinProximityTarget>().ToList();

        // Perform the initial update and sort
        UpdateCoinVisibilityAndSort();
        Debug.Log("\n--- Initial Coin Visibility and Sort ---");
        PrintCoinList();

        _timer = updateInterval; // Initialize timer
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            UpdateCoinVisibilityAndSort();
            Debug.Log($"\n--- Coins Updated and Re-Sorted at {Time.timeSinceLevelLoad:F2}s ---");
            PrintCoinList();
            _timer = updateInterval; // Reset timer
        }
    }

    /// <summary>
    /// Updates the distance of each coin to the player, then sorts them by proximity
    /// using Bubble Sort, and finally sets their visibility based on the display range.
    /// </summary>
    public void UpdateCoinVisibilityAndSort()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Cannot update coin proximity: Player Transform is null.");
            return;
        }

        // Clean up list by removing any null references (e.g., if coins were collected/destroyed)
        _activeCoins.RemoveAll(item => item == null);

        // Step 1: Update the DistanceToPlayer for each coin
        Vector3 currentPlayerPosition = playerTransform.position;
        foreach (CoinProximityTarget coin in _activeCoins)
        {
            if (coin != null)
            {
                coin.DistanceToPlayer = Vector3.Distance(currentPlayerPosition, coin.transform.position);
            }
        }

        // Step 2: Perform Bubble Sort on the list based on DistanceToPlayer
        // Sorts the coins from closest to furthest
        int n = _activeCoins.Count;
        bool swapped; // Flag to check if any swaps occurred in a pass

        for (int i = 0; i < n - 1; i++)
        {
            swapped = false; // Reset for each pass
            for (int j = 0; j < n - i - 1; j++)
            {
                // Compare adjacent coins based on DistanceToPlayer
                // We want to sort in ascending order (closest first)
                if (_activeCoins[j].DistanceToPlayer > _activeCoins[j + 1].DistanceToPlayer)
                {
                    // If the current coin is further than the next one, swap them
                    CoinProximityTarget temp = _activeCoins[j];
                    _activeCoins[j] = _activeCoins[j + 1];
                    _activeCoins[j + 1] = temp;
                    swapped = true; // Mark that a swap occurred
                }
            }

            // Optimization: If no two elements were swapped by the inner loop,
            // it means the list is already sorted, so we can stop early.
            if (!swapped)
            {
                break;
            }
        }

        // Step 3: Set visibility based on the displayRange
        foreach (CoinProximityTarget coin in _activeCoins)
        {
            if (coin != null)
            {
                // Coins within the displayRange are visible, others are hidden.
                coin.SetVisibility(coin.DistanceToPlayer <= displayRange);
            }
        }
    }

    /// <summary>
    /// Helper method to print the current state of the coin list to the console.
    /// </summary>
    void PrintCoinList()
    {
        if (_activeCoins.Count == 0)
        {
            Debug.Log("No active coins to display.");
            return;
        }
        foreach (CoinProximityTarget coin in _activeCoins)
        {
            if (coin != null)
            {
                Debug.Log(coin.ToString());
            }
        }
    }

    /// <summary>
    /// Public method to get the closest visible coin.
    /// You might use this for a UI indicator or auto-pickup logic.
    /// </summary>
    /// <returns>The closest visible coin, or null if none are visible.</returns>
    public CoinProximityTarget GetClosestVisibleCoin()
    {
        // Ensure the list is sorted and visibility is updated before querying
        UpdateCoinVisibilityAndSort();

        foreach (CoinProximityTarget coin in _activeCoins)
        {
            // If the coin is within display range, it's considered visible.
            // Since the list is sorted by proximity, the first one found is the closest.
            if (coin != null && coin.DistanceToPlayer <= displayRange)
            {
                return coin;
            }
        }
        return null; // No visible coins
    }
}