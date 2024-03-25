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
            GameObject noteToJudge = notesInZone[0];
            float distanceToCenter = Vector2.Distance(noteToJudge.transform.position, transform.position);
            string judgment = GetJudgment(distanceToCenter);
            Debug.Log($"{judgment} for Note");

            // 판정된 노트 제거
            notesInZone.RemoveAt(0);
            Destroy(noteToJudge);
        }
        else
        {
            Debug.Log("No notes to judge!");
        }
    }

    string GetJudgment(float distance)
    {
        // 판정 영역의 반경을 기준으로 점수 계산
        float maxDistance = circleCollider.radius;
        float distancePercentage = distance / maxDistance;

        if (distancePercentage < 0.25f)
        {
            return "Perfect";
        }
        else if (distancePercentage < 0.5f)
        {
            return "Great";
        }
        else if (distancePercentage < 0.75f)
        {
            return "Good";
        }
        else
        {
            return "Miss";
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