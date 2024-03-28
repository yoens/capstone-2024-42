using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float growthRate = 0.5f; // 노트가 커지는 속도
    public float maxScale = 3.0f; // 노트의 최대 크기
    private Vector2 initialScale = new Vector2(1.0f, 1.0f); // 노트의 초기 크기

    public SpriteRenderer spriteRenderer; // 노트의 SpriteRenderer 컴포넌트
    public Sprite[] noteSprites; // 방향에 따른 노트의 스프라이트 배열 (1,2,3,4)

    private Vector2[] directions = new Vector2[] // 4가지 방향 정의 (1,2,3,4)
    {
        new Vector2(2.2f, 3f), // 1사분면
        new Vector2(-2.2f, 3f), // 2사분면
        new Vector2(-2.2f, -3f), // 3사분면
        new Vector2(2.2f, -3f) // 4사분면
    };

    private Vector3 moveDirection; // 노트가 이동할 방향

    void Start()
    {
        transform.localScale = initialScale; // 노트의 초기 크기 설정
        SetDirectionAndSprite(); // 노트의 방향과 스프라이트 설정
    }

    void Update()
    {
        // 노트 크기 증가
        if (transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(growthRate, growthRate, 0) * Time.deltaTime;
        }

        // 노트 이동
        transform.Translate(moveDirection * growthRate * Time.deltaTime, Space.World);
    }

    private void SetDirectionAndSprite()
    {
        int directionIndex = Random.Range(0, directions.Length); // 4가지 방향 중 하나를 랜덤으로 선택
        moveDirection = directions[directionIndex]; // 이동 방향 설정

        // 선택된 방향에 맞는 스프라이트를 할당
        spriteRenderer.sprite = noteSprites[directionIndex];
    }
}