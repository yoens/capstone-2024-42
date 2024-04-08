using UnityEngine;

public class DragNoteMovement : MonoBehaviour
{
    public float speed = 5f; // 이동 속도
    private Vector3 moveDirection; // 이동 방향

    // 이동 방향과 속도를 설정하는 메소드
    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized; // 방향을 정규화하여 저장
    }

    void Update()
    {
        // 매 프레임마다 이동 방향으로 이동
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
