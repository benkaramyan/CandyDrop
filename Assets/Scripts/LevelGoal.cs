using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    public CandyTracker candyTracker;
    public GameObject winPanel;
    public LevelDatabase levelDatabase; // Reference to the level database
    public int currentLevelIndex = 0;   // Set the current level index

    private LevelData currentLevelData;
    private bool hasWon = false;

    private void Start()
    {
        if (candyTracker == null)
        {
            Debug.LogWarning("CandyTracker reference is missing on LevelGoal.");
        }

        if (levelDatabase != null && currentLevelIndex < levelDatabase.levels.Count)
        {
            currentLevelData = levelDatabase.levels[currentLevelIndex];
        }
        else
        {
            Debug.LogError("Invalid level index or missing level database.");
        }

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    public void CheckWinCondition()
    {
        if (hasWon || currentLevelData == null) return;

        foreach (var goal in currentLevelData.goals)
        {
            int currentCount = candyTracker.GetCandyCount(goal.level);
            if (currentCount < goal.quantity)
            {
                return; // Not all goals met yet
            }
        }

        hasWon = true;
        Debug.Log("Level completed!");

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Time.timeScale = 0; // Pause the game
    }
}