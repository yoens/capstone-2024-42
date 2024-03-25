using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentLine : MonoBehaviour
{
    public EdgeCollider2D[] missZones; // Miss 판정을 위한 EdgeCollider2D 배열
    public CircleCollider2D hitZone; // Hit 판정을 위한 CircleCollider2D
    private float hitRadius = 3.0f; // CircleCollider2D의 반지름

    void Start()
    {
        hitZone = GetComponent<CircleCollider2D>(); // CircleCollider2D 컴포넌트 할당
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // EdgeCollider2D에 닿았을 때 Miss 판정
        foreach (var zone in missZones)
        {
            if (other.IsTouching(zone))
            {
                Debug.Log("Miss");
                Destroy(other.gameObject); // 노트 제거
                return; // 루프에서 벗어남
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Note2"))
        {
            // 노트와 CircleCollider2D 중심 사이의 거리를 계산
            float distanceToCenter = Vector2.Distance(other.transform.position, hitZone.transform.position);
            
            // 거리가 CircleCollider2D의 반지름과 일치할 때 Hit 판정
            if (Mathf.Abs(distanceToCenter - hitRadius) <= 0.1f) // 약간의 오차를 허용
            {
                Debug.Log("Hit");
                Destroy(other.gameObject); // 노트 제거
            }
        }
    }
}