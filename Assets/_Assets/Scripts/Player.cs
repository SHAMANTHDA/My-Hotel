using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Vector3 _startPosition;
    private bool _isDragging = false;

    private void Update()
    {
        HandlePointerInput();
    }

    #region keyboardInput
    //private void HandleKeyboardInput()
    //{
    //    Vector2 inputVector = new Vector2(0, 0);

    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        inputVector.y = +1;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        inputVector.y = -1;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        inputVector.x = -1;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        inputVector.x = +1;
    //    }

    //    Move(inputVector.normalized);
    //}
    #endregion

    #region mouse & touch input
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
            Vector2 delta = (Vector2)(Input.mousePosition - _startPosition);
            Move(delta.normalized);
            _startPosition = Input.mousePosition;
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
                        Vector2 delta = touch.position - new Vector2(_startPosition.x, _startPosition.y);
                        Move(delta.normalized);
                        _startPosition = new Vector3(touch.position.x, touch.position.y, 0);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isDragging = false;
                    break;
            }
        }
    }

    private void Move(Vector2 inputVector)
    {
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * _moveSpeed * Time.deltaTime;

        transform.forward = moveDir;
    }
    #endregion
}
