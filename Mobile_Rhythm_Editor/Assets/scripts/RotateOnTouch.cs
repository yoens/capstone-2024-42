using UnityEngine;

public class RotateOnDrag : MonoBehaviour
{
    private Vector2 previousTouchPosition;
    private bool isDragging = false;

    public float rotationSpeed = 100f; // 회전 속도 조절

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    previousTouchPosition = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector2 currentTouchPosition = touch.position;
                        float deltaX = currentTouchPosition.x - previousTouchPosition.x;

                        if (deltaX > 0)
                        {
                            // x값이 증가하면 시계 반대 방향으로 회전
                            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
                        }
                        else if (deltaX < 0)
                        {
                            // x값이 감소하면 시계 방향으로 회전
                            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                        }

                        previousTouchPosition = currentTouchPosition;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
    }
}
