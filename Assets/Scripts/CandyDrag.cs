using UnityEngine;

public class CandyDrag : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private bool hasBeenReleased = false;
    private CandySpawner candySpawner; // Reference to the spawner

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure gravity is set to 0 initially so it doesn't fall
        rb.gravityScale = 0;
    }

    void Update()
    {
        // Only handle input if the candy has not been released
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
        if (Input.GetMouseButtonDown(0) && !hasBeenReleased) // Left-click
        {
            StartDragging();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            StopDragging();
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
        hasBeenReleased = true;    // Mark as released so it cannot be dragged again
        rb.gravityScale = 1;       // Enable gravity to make the candy fall

        // Trigger the spawner's delay before the next spawn
        if (candySpawner != null)
        {
            candySpawner.StartCoroutine(candySpawner.DelayNextSpawn());
        }
    }

    // Method to set the spawner reference
    public void SetSpawner(CandySpawner spawner)
    {
        candySpawner = spawner;
    }
}