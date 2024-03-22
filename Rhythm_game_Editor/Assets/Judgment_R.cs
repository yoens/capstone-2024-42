using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgment_R : MonoBehaviour
{
    private List<Note> notesInLine = new List<Note>();
    private float judgmentDistanceRange = 2.0f; // 사용자 입력을 기다리는 최대 거리 범위

    // 판정 범위 정의
    private float perfectRange = 0.1f; // "Perfect" 판정을 위한 최대 거리
    private float greatRange = 0.2f; // "Great" 판정을 위한 최대 거리
    private float goodRange = 0.3f; // "Good" 판정을 위한 최대 거리

    private void Update()
    {
        // 사용자가 스페이스바를 누를 때 판정 실행
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            JudgeClosestNote();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Note note = other.GetComponent<Note>();
        if (note != null && !note.judged)
        {
            notesInLine.Add(note); // 판정선에 있는 노트 추가
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Note note = other.GetComponent<Note>();
        if (note != null)
        {
            notesInLine.Remove(note); // 판정선을 벗어난 노트 제거
            if (!note.judged)
            {
                Debug.Log("Miss");
                GameManager.instance.AddScore(0);
                note.judged = true;
                Destroy(other.gameObject);
            }
        }
    }

    // 가장 가까운 노트를 판정
    private void JudgeClosestNote()
    {
        Note closestNote = null;
        float closestDistance = float.MaxValue;

        // 판정선에 있는 모든 노트를 순회하며, 가장 거리가 작은 노트를 찾습니다.
        foreach (var note in notesInLine)
        {
            if (note.judged) continue; // 이미 판정된 노트는 무시

            float distance = Vector2.Distance(transform.position, note.transform.position);

            // 가장 가까운 노트를 찾습니다.
            if (distance < closestDistance)
            {
                closestNote = note;
                closestDistance = distance;
            }
        }

        // 가장 가까운 노트를 판정
        if (closestNote != null && closestDistance <= judgmentDistanceRange)
        {
            JudgeNote(closestDistance, closestNote);
            notesInLine.Remove(closestNote); // 판정된 노트를 리스트에서 제거
        }
        else
        {
            Debug.Log("No note to judge or all notes out of range.");
        }
    }

    private void JudgeNote(float distance, Note note)
    {
        if (note.judged) return; // 이미 판정된 노트는 처리하지 않음

        // 판정 범위를 기반으로 판정
        if (distance <= perfectRange)
        {
            Debug.Log("Perfect!");
            GameManager.instance.AddScore(100);
        }
        else if (distance <= greatRange)
        {
            Debug.Log("Great");
            GameManager.instance.AddScore(75);
        }
        else if (distance <= goodRange)
        {
            Debug.Log("Good");
            GameManager.instance.AddScore(50);
        }
        else
        {
            Debug.Log("Miss");
            GameManager.instance.AddScore(0);
        }

        note.judged = true; // 판정 완료 표시
        Destroy(note.gameObject); // 노트 제거
    }
}