using UnityEngine;

public class CandyMergeHandler : MonoBehaviour
{
    [SerializeField] private CandyLevelManager levelManager;
    [SerializeField] private CandyTracker candyTracker;
    [SerializeField] private LevelGoal levelGoal;

    public void SpawnNextLevelCandy(int level, Vector2 spawnPosition)
    {
        if (levelManager != null)
        {
            GameObject nextLevelPrefab = levelManager.GetCandyPrefabByLevel(level);
            if (nextLevelPrefab != null)
            {
                GameObject mergedCandy = Instantiate(nextLevelPrefab, spawnPosition, Quaternion.identity);

                Rigidbody2D mergedRb = mergedCandy.GetComponent<Rigidbody2D>();
                if (mergedRb != null)
                {
                    mergedRb.gravityScale = 1;
                }

                CandyDrag dragComponent = mergedCandy.GetComponent<CandyDrag>();
                if (dragComponent != null)
                {
                    Destroy(dragComponent);
                }

                // Track the new candy in CandyTracker and check win conditions
                if (candyTracker != null)
                {
                    candyTracker.AddCandy(level);
                }

                if (levelGoal != null)
                {
                    levelGoal.CheckWinCondition();
                }
            }
        }
    }
}