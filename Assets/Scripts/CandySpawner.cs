using UnityEngine;
using System.Collections;

public class CandySpawner : MonoBehaviour
{
    public GameObject[] candyPrefabs;   // Array to hold multiple candy prefabs
    private GameObject currentCandy;    // Track the currently active candy

    void Start()
    {
        // Spawn the first candy at the start of the game
        SpawnCandy();
    }

    public void SpawnCandy()
    {
        // Only spawn a new candy if there isn't one already
        if (currentCandy == null && candyPrefabs.Length > 0)
        {
            // Choose a random candy prefab from the array
            GameObject selectedCandy = candyPrefabs[Random.Range(0, candyPrefabs.Length)];

            // Spawn the selected candy prefab at the spawner's position
            currentCandy = Instantiate(selectedCandy, transform.position, Quaternion.identity);

            // Link the spawner to the candy's drag script
            CandyDrag candyDrag = currentCandy.GetComponent<CandyDrag>();
            candyDrag.SetSpawner(this);
        }
    }

    // Called when the current candy is released and ready for the next spawn
    public IEnumerator DelayNextSpawn()
    {
        yield return new WaitForSeconds(1f); // 1-second delay
        currentCandy = null;                   // Reset current candy reference
        SpawnCandy();                          // Spawn the next candy
    }
}