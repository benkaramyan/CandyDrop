using UnityEngine;
using UnityEngine.UI;

public class GoalItemController : MonoBehaviour
{
    public Image candyImage; // Image representing the candy type
    public Text progressText; // Text to show current progress
    public GameObject checkmark; // Checkmark to show when the goal is met

    private int goalLevel;
    private int remainingCount;

    // Initialize the goal item with level and quantity data
    public void Initialize(int level, int count, Sprite candySprite)
    {
        goalLevel = level;
        remainingCount = count;
        candyImage.sprite = candySprite;
        progressText.text = $"{remainingCount}"; // Show the initial count
        checkmark.SetActive(false); // Hide the checkmark initially
    }

    // Update the progress text and checkmark based on current progress
    public void UpdateProgress(int currentCount)
    {
        remainingCount = currentCount;
        progressText.text = $"{remainingCount}"; // Update the count directly

        if (remainingCount <= 0)
        {
            progressText.gameObject.SetActive(false); // Hide the text when completed
            checkmark.SetActive(true); // Show the checkmark when the goal is met
        }
    }
}