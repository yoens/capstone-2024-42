using UnityEngine;

public class RotateOnTouch : MonoBehaviour
{
    public Transform centerPoint;  // 회전의 중심점을 설정합니다.
    public float rotationSpeed = 100f;  // 회전 속도를 조절합니다.
    public float sensitivity = 0.5f;  // 마우스 움직임에 대한 민감도를 조절합니다.

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // 마우스 X축 움직임을 기준으로 회전량을 계산하되, 민감도를 적용합니다.
                float rotation = Input.GetAxis("Mouse X") * rotationSpeed * sensitivity * Time.deltaTime;
                transform.RotateAround(centerPoint.position, Vector3.forward, rotation);
            }
        }
    }
}
