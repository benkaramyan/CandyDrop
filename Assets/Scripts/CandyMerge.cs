using UnityEngine;

public class CandyMerge : MonoBehaviour
{
    public int level; // Level of the candy, retrieved from CandyLevel script
    private bool hasMerged = false;

    private void Start()
    {
        // Ensure level is correctly set from the CandyLevel component
        CandyLevel candyLevelComponent = GetComponent<CandyLevel>();
        if (candyLevelComponent != null)
        {
            level = candyLevelComponent.level;
        }
        else
        {
            Debug.LogWarning("CandyLevel component missing on prefab.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMerged) return; // Prevent further merging if already merged

        // Check if the collision is with another candy
        if (collision.gameObject.CompareTag("Candy"))
        {
            CandyMerge otherCandy = collision.gameObject.GetComponent<CandyMerge>();

            // Check both the level and merging status
            if (otherCandy != null && !otherCandy.hasMerged && otherCandy.level == level)
            {
                Debug.Log($"Merging candies of level {level} into level {level + 1}");

                // Set flags to prevent both candies from merging again
                hasMerged = true;
                otherCandy.hasMerged = true;

                // Call the CandyMergeHandler to spawn the next level candy
                CandyMergeHandler mergeHandler = FindObjectOfType<CandyMergeHandler>();
                if (mergeHandler != null)
                {
                    Vector3 spawnPosition = transform.position + Vector3.up * 0.2f; // Adjust to avoid overlap
                    Debug.Log("Calling CandyMergeHandler to spawn next level candy.");
                    mergeHandler.SpawnNextLevelCandy(level + 1, spawnPosition);
                }
                else
                {
                    Debug.LogWarning("CandyMergeHandler not found in scene.");
                }

                // Destroy both candies involved in the merge
                Destroy(otherCandy.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Collision detected, but conditions for merging were not met.");
            }
        }
    }
}