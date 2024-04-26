using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Vector3 _startPosition;
    private Vector3 _movementDirection;
    private bool _isDragging = false;

    private bool isWalking;

    private void Update()
    {
        HandlePointerInput();
    }

    private void HandlePointerInput()
    {
        // Handle Desktop Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
            _isDragging = true;
        }
        if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector2 delta = currentMousePosition - _startPosition;
            if (delta.magnitude > 0.1) // Use a small threshold to update direction only if there's significant movement
            {
                _movementDirection = new Vector3(delta.x, 0, delta.y).normalized;
                _startPosition = currentMousePosition; // Update start position to current for smooth continuous update
            }
            Move(_movementDirection); // Move in the last updated direction
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        // Handle Mobile Touch Input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPosition = new Vector3(touch.position.x, touch.position.y, 0);
                    _isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (_isDragging)
                    {
                        Vector3 currentTouchPosition = touch.position;
                        Vector2 delta = currentTouchPosition - _startPosition;
                        if (delta.magnitude > 0.1) // Use a small threshold to update direction only if there's significant movement
                        {
                            _movementDirection = new Vector3(delta.x, 0, delta.y).normalized;
                            _startPosition = currentTouchPosition; // Update start position to current for smooth continuous update
                        }
                        Move(_movementDirection); // Move in the last updated direction
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isDragging = false;
                    break;
            }
        }
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction * _moveSpeed * Time.deltaTime;
        if (direction != Vector3.zero)
        {
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed); // Make sure the player faces the direction of movement
        }
    }

    public bool IsWalking()
    {
        isWalking = _isDragging && _movementDirection != Vector3.zero;
        return isWalking;
    }
}
