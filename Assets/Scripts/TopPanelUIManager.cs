using System.Collections.Generic;
using UnityEngine;

public class TopPanelUIManager : MonoBehaviour
{
    public LevelGoal levelGoal; // Reference to LevelGoal to access level data
    public GameObject goalItemPrefab; // Prefab for the GoalItem UI
    public Transform goalPanel; // The parent panel for the GoalItems

    private Dictionary<int, GoalItemController> goalItems = new Dictionary<int, GoalItemController>();

    private void Start()
    {
        LoadLevelGoals();
    }

    private void LoadLevelGoals()
    {
        var currentLevelData = levelGoal.levelDatabase.levels[levelGoal.currentLevelIndex];

        foreach (var goal in currentLevelData.goals)
        {
            GameObject goalItem = Instantiate(goalItemPrefab, goalPanel);
            GoalItemController controller = goalItem.GetComponent<GoalItemController>();

            if (controller != null)
            {
                // Load the sprite from the Resources folder
                GameObject candyPrefab = Resources.Load<GameObject>($"Candy{goal.level}");
                Sprite candySprite = null;
                if (candyPrefab != null)
                {
                    SpriteRenderer spriteRenderer = candyPrefab.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        candySprite = spriteRenderer.sprite;
                    }
                }

                if (candySprite != null)
                {
                    controller.Initialize(goal.level, goal.quantity, candySprite);
                    goalItems[goal.level] = controller;
                }
                else
                {
                    Debug.LogWarning($"Sprite for Candy{goal.level} not found.");
                }
            }
        }
    }

    // Method to update the progress of the goal items
    public void UpdateGoalProgress(int level, int currentCount)
    {
        if (goalItems.ContainsKey(level))
        {
            goalItems[level].UpdateProgress(currentCount);
        }
    }
}