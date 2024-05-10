using UnityEngine;

public class RotateOnTouch : MonoBehaviour
{
    public Transform centerPoint;  // 회전의 중심점을 설정합니다.
    public RandomMarkers randomMarkers;  // RandomMarkers 스크립트 참조
    public float rotationSpeed = 100f;  // 회전 속도를 조절합니다.
    public float sensitivity = 0.5f;  // 터치 움직임에 대한 민감도를 조절합니다.
    public CircleCollider2D touchCollider;  // 원판의 Collider2D

   void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;  // Z축 조정

            if (touch.phase == TouchPhase.Moved && touchCollider.OverlapPoint(touchPos))
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                float rotation = touchDeltaPosition.x * rotationSpeed * sensitivity * Time.deltaTime;
                transform.RotateAround(centerPoint.position, Vector3.forward, -rotation);

                // 현재 각도 계산 및 RandomMarkers 스크립트에 전달
                float currentAngle = transform.eulerAngles.z;
                if (randomMarkers != null)
                {
                    randomMarkers.UpdateRotation(currentAngle);
                }
            }
        }
    }
}
