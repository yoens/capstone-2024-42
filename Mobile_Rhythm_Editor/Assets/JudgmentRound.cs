using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentRound : MonoBehaviour
{
    private List<GameObject> notesInZone = new List<GameObject>();
    private CircleCollider2D circleCollider;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void JudgeNoteOnButtonClick()
    {
        if (notesInZone.Count > 0)
        {
            GameObject noteToJudge = notesInZone[0]; // 예시로 첫 번째 노트를 판정합니다.
            // 판정 원 중심부터 노트까지의 거리 계산
            float distanceToCenter = Vector2.Distance(noteToJudge.transform.position, transform.position);
            // 원 가장자리까지의 거리를 기준으로 판정 범위 계산
            float distanceToEdge = circleCollider.radius - distanceToCenter;
            
            // 거리에 따른 판정 로직
            if (distanceToEdge < 0.1f) // 원 가장자리에 매우 가까움
            {
                Debug.Log("Perfect!");
            }
            else if (distanceToEdge < 0.2f)
            {
                Debug.Log("Great");
            }
            else if (distanceToEdge < 0.3f)
            {
                Debug.Log("Good");
            }
            else
            {
                Debug.Log("Miss"); // 원 중심에 가깝거나 판정 영역 밖
            }

            // 노트 제거 및 리스트에서 제거
            notesInZone.RemoveAt(0);
            Destroy(noteToJudge);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Note"))
        {
            notesInZone.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Note"))
        {
            notesInZone.Remove(collider.gameObject);
        }
    }
}