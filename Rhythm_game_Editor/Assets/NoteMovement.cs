using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    private Vector3 moveDirection; // 이동 방향

    // 이동 방향 설정 메서드
    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    void Update()
    {
        // 설정된 방향으로 노트 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
