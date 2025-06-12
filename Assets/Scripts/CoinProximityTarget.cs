using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to attach to each coin GameObject that needs its visibility
/// managed based on player proximity. Designed for 2D sprites.
/// </summary>
public class CoinProximityTarget : MonoBehaviour
{
// name of coin
    public string CoinName = "Coin";

    // The calculated distance from this coin to the player.
    // This value is updated by the CoinProximityManager.
    [HideInInspector] // Hide from Inspector as it's updated dynamically
    public float DistanceToPlayer = 0f;

    // Reference to the coin's SpriteRenderer component.
    // We'll use this to enable/disable the coin's visual.
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        // Get the SpriteRenderer component attached to this GameObject.
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogWarning($"Coin '{CoinName}' (GameObject: {gameObject.name}) does not have a SpriteRenderer. " +
                             "Its visibility cannot be controlled by this script. " +
                             "Ensure 2D coin has a SpriteRenderer component.");
        }
    }

    /// <summary>
    /// Sets the visibility of the coin's SpriteRenderer.
    /// </summary>
    /// <param name="isVisible">True to show the coin, false to hide it.</param>
    public void SetVisibility(bool isVisible)
    {
        if (_spriteRenderer != null && _spriteRenderer.enabled != isVisible)
        {
            _spriteRenderer.enabled = isVisible;
            // Debug.Log($"Coin '{CoinName}' visibility set to: {isVisible}");
        }
    }

    public override string ToString()
    {
        // Formats the distance to one decimal place for cleaner output
        return $"{CoinName} (Distance: {DistanceToPlayer:F1}m, Visible: {_spriteRenderer != null && _spriteRenderer.enabled})";
    }
}
