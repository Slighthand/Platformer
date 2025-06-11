using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySort : MonoBehaviour
{
    public class Quest
    {
        public string Title;
        public Vector3 TargetLocation; // The coordinates of the quest objective
        public float ProximityToPlayer; // This value is updated

        public Quest(string title, Vector3 targetLocation)
        {
            this.Title = title;
            this.TargetLocation = targetLocation;
            this.ProximityToPlayer = 0f; 
        }

        public override string ToString()
        {
            // Format ProximityToPlayer to one decimal place for cleaner output
            return $"{Title} (Distance: {ProximityToPlayer:F1}m)";
        }
    }
    public string Title;
    public Vector3 TargetLocation; // The world coordinates of the quest objective
    public float ProximityToPlayer;
    public Transform playerTransform;
    public List<Quest> activeQuests = new List<Quest>();
    // sort enemies by distance
    public void SortQuestsByProximity()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Cannot sort quests: Player Transform is null.");
            return;
        }

        // Step 1: Update the ProximityToPlayer for each quest
        // This must happen BEFORE sorting, as distances change with player movement.
        Vector3 currentPlayerPosition = playerTransform.position;
        foreach (Quest quest in activeQuests)
        {
            quest.ProximityToPlayer = Vector3.Distance(currentPlayerPosition, quest.TargetLocation);
        }

        // Step 2: Perform Bubble Sort on the list based on ProximityToPlayer
        int n = activeQuests.Count;
        bool swapped; // Flag to check if any swaps occurred in a pass

        for (int i = 0; i < n - 1; i++)
        {
            swapped = false; // Reset for each pass
            for (int j = 0; j < n - i - 1; j++)
            {
                // Compare adjacent quests based on ProximityToPlayer
                // We want to sort in ascending order (closest first)
                if (activeQuests[j].ProximityToPlayer > activeQuests[j + 1].ProximityToPlayer)
                {
                    // If the current quest is further than the next one, swap them
                    Quest tmp = activeQuests[j];
                    activeQuests[j] = activeQuests[j + 1];
                    activeQuests[j + 1] = tmp;
                    swapped = true; // Mark that a swap occurred
                }
            }

            // Optimization: If no two elements were swapped by the inner loop,
            // it means the list is already sorted, so we can stop early.
            if (swapped == false)
            {
                break;
            }
        }
    }

}
