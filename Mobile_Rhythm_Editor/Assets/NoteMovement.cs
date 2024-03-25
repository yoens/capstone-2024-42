using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public Vector2 moveDirection; // 이동 방향
    public float moveSpeed = 5f;  // 이동 속도

    private void Start()
    {
        // 초기 방향을 랜덤으로 설정
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    private void Update()
    {
        // 매 프레임마다 노트를 이동시킵니다.
        transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
    }

    // 외부에서 이동 방향을 설정하기 위한 메서드 (옵션)
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }
}