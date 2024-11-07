using UnityEngine;

public class CandyDrag : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private bool hasBeenReleased = false;
    private CandySpawner candySpawner; // Reference to the spawner

    private Vector2 releaseForce; // Private field to store release force

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Ensure gravity is set to 0 initially so it doesn't fall
    }

    void Update()
    {
        if (!hasBeenReleased)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
            HandleTouchInput();
#endif
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !hasBeenReleased)
        {
            StartDragging();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            StopDragging();
            ApplyReleaseForce(); // Apply force when the click is released
            NotifySpawnerForNextSpawn(); // Notify the spawner to start delay
        }

        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x, transform.position.y);
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0 && !hasBeenReleased)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartDragging();
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                StopDragging();
                ApplyReleaseForce(); // Apply force when touch is released
                NotifySpawnerForNextSpawn(); // Notify the spawner to start delay
            }

            if (isDragging)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                transform.position = new Vector2(touchPosition.x, transform.position.y);
            }
        }
    }

    private void StartDragging()
    {
        isDragging = true;
    }

    private void StopDragging()
    {
        isDragging = false;
        hasBeenReleased = true; // Mark as released so it cannot be dragged again
        rb.gravityScale = 1;    // Enable gravity to make the candy fall
    }

    private void ApplyReleaseForce()
    {
        if (rb != null)
        {
            rb.AddForce(releaseForce, ForceMode2D.Impulse); // Apply the force to make the candy move
        }
    }

    // Method to set the spawner reference
    public void SetSpawner(CandySpawner spawner)
    {
        candySpawner = spawner;
    }

    // Method to set the release force from the spawner
    public void SetReleaseForce(Vector2 force)
    {
        releaseForce = force;
    }

    private void NotifySpawnerForNextSpawn()
    {
        if (candySpawner != null)
        {
            candySpawner.StartCoroutine(candySpawner.DelayNextSpawn()); // Trigger the spawner to start delay for the next candy
        }
    }
}