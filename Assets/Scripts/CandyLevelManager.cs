using UnityEngine;

public class CandyLevelManager : MonoBehaviour
{
    // Array to hold candy prefabs in level order; levels inferred from index
    [SerializeField] private GameObject[] candyPrefabs;

    // Method to get a candy prefab by level
    public GameObject GetCandyPrefabByLevel(int level)
    {
        if (level >= 0 && level < candyPrefabs.Length)
        {
            return candyPrefabs[level];
        }
        Debug.LogWarning($"Requested level {level} is out of range.");
        return null; // Return null if the level is invalid
    }

    // Method to get the level of a candy based on its prefab
    public int GetCandyLevel(GameObject candy)
    {
        for (int i = 0; i < candyPrefabs.Length; i++)
        {
            if (candyPrefabs[i] == candy)
            {
                return i; // The index matches the level
            }
        }
        Debug.LogWarning("Candy prefab not found in level manager.");
        return -1; // Return -1 if candy is not found
    }
}