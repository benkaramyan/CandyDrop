using System.Collections.Generic;
using UnityEngine;

public class CandyTracker : MonoBehaviour
{
    private Dictionary<int, int> candyCounts = new Dictionary<int, int>(); // Tracks the number of candies collected per level
    public TopPanelUIManager uiManager; // Reference to update the UI
    [SerializeField] private LevelGoal levelGoal; // Reference to check level goals

    private void Start()
    {
        // Check for missing references
        if (uiManager == null)
        {
            Debug.LogError("UI Manager is not assigned in CandyTracker.");
        }
        if (levelGoal == null)
        {
            Debug.LogError("LevelGoal is not assigned in CandyTracker.");
        }
    }

    // Method to add a collected candy
    public void AddCandy(int level)
    {
        if (!candyCounts.ContainsKey(level))
        {
            candyCounts[level] = 0;
        }
        candyCounts[level]++;

        // Debug logs for checking potential null references
        if (levelGoal == null)
        {
            Debug.LogError("LevelGoal is null in AddCandy.");
            return; // Exit to prevent further null reference errors
        }
        else if (levelGoal.levelDatabase == null)
        {
            Debug.LogError("LevelDatabase is null in AddCandy.");
            return; // Exit to prevent further null reference errors
        }

        // Retrieve the goal count for the given level
        var goal = levelGoal.levelDatabase.levels[levelGoal.currentLevelIndex].goals.Find(g => g.level == level);
        if (goal == null)
        {
            Debug.LogError($"Goal for level {level} not found in current level data.");
            return; // Exit if no goal is found
        }

        int goalCount = goal.quantity;
        int remainingCount = goalCount - candyCounts[level];

        Debug.Log($"Candy collected at level {level}. Remaining count: {remainingCount}");

        // Update the UI progress
        if (uiManager != null)
        {
            uiManager.UpdateGoalProgress(level, remainingCount);
        }
        else
        {
            Debug.LogError("UI Manager reference is missing in CandyTracker.");
        }

        // Check if the goal for this level is met
        CheckWinCondition();
    }

    // Method to get the current candy count for a specific level
    public int GetCandyCount(int level)
    {
        return candyCounts.ContainsKey(level) ? candyCounts[level] : 0;
    }

    // Method to check if all goals are completed
    private void CheckWinCondition()
    {
        if (levelGoal == null || levelGoal.levelDatabase == null)
        {
            Debug.LogError("LevelGoal or LevelDatabase is not assigned in CandyTracker.");
            return;
        }

        var currentLevelData = levelGoal.levelDatabase.levels[levelGoal.currentLevelIndex];
        foreach (var goal in currentLevelData.goals)
        {
            if (!candyCounts.ContainsKey(goal.level) || candyCounts[goal.level] < goal.quantity)
            {
                // If any goal is not met, return early
                return;
            }
        }

        // If all goals are met, trigger the win condition
        Debug.Log("All goals met! You win!");
        // Add your game-winning logic here (e.g., show a "You Win" screen)
    }
}