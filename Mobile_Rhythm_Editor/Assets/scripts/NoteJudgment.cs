using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NoteJudgment : MonoBehaviour
{
    // 각 노트 타입별 판정선을 선언합니다.
    public Transform judgmentLine_Y;
    public Transform judgmentLine_G;
    public Transform judgmentLine_R;

    public float perfectThreshold = 0.05f; // Perfect 판정 거리
    public float greatThreshold = 0.1f; // Great 판정 거리
    public float goodThreshold = 0.15f; // Good 판정 거리
    public float missThreshold = 0.2f; // Miss 판정 거리

    private List<GameObject> notesInPlay = new List<GameObject>(); // 활성화된 노트들의 리스트

    void Update()
    {
        UpdateNotesInPlay();
        CheckMissedNotes();
    }

    void UpdateNotesInPlay()
    {
        notesInPlay.Clear();
        notesInPlay.AddRange(GameObject.FindGameObjectsWithTag("Note_Y"));
        notesInPlay.AddRange(GameObject.FindGameObjectsWithTag("Note_G"));
        notesInPlay.AddRange(GameObject.FindGameObjectsWithTag("Note_R"));
    }

    void CheckMissedNotes()
    {
        // 각 노트 타입별로 미스를 체크합니다.
        for (int i = notesInPlay.Count - 1; i >= 0; i--)
        {
            var note = notesInPlay[i];
            Transform specificJudgmentLine = null;

            // 노트 타입에 따라 사용할 판정선을 결정합니다.
            if (note.tag == "Note_Y")
            {
                specificJudgmentLine = judgmentLine_Y;
            }
            else if (note.tag == "Note_G")
            {
                specificJudgmentLine = judgmentLine_G;
            }
            else if (note.tag == "Note_R")
            {
                specificJudgmentLine = judgmentLine_R;
            }

            // 지정된 판정선이 있을 경우, 해당 노트가 판정선을 넘었는지 확인합니다.
            if (specificJudgmentLine != null && note.transform.position.y < specificJudgmentLine.position.y - missThreshold)
            {
                Debug.Log("Miss by distance: " + note.tag);
                Destroy(note);
                notesInPlay.RemoveAt(i);
            }
        }    
    }

    // 버튼 클릭 이벤트에 의해 호출될 새로운 메소드들입니다.
    public void JudgeClosestNote_Y()
    {
        JudgeClosestNote("Note_Y", judgmentLine_Y);
    }

    public void JudgeClosestNote_G()
    {
        JudgeClosestNote("Note_G", judgmentLine_G);
    }

    public void JudgeClosestNote_R()
    {
        JudgeClosestNote("Note_R", judgmentLine_R);
    }

    // JudgeClosestNote 메소드에 특정 판정선을 매개변수로 받도록 변경합니다.
    private void JudgeClosestNote(string noteType, Transform specificJudgmentLine)
    {
        var notes = notesInPlay.Where(note => note.tag == noteType).ToList();
        GameObject closestNote = null;
        float minDistance = float.MaxValue;

        foreach (GameObject note in notes)
        {
            float distance = Mathf.Abs(note.transform.position.y - specificJudgmentLine.position.y);
            if (distance < minDistance)
            {
                closestNote = note;
                minDistance = distance;
            }
        }

        if (closestNote != null && minDistance <= goodThreshold)
        {
            string judgment = DetermineJudgment(minDistance);
            Debug.Log(noteType + " " + judgment);
            Destroy(closestNote);
            notesInPlay.Remove(closestNote);
        }
    }

    private string DetermineJudgment(float distance)
    {
        if (distance <= perfectThreshold)
        {
            ScoreManager.Instance.AddScore(100);
            ComboManager.Instance.AddCombo(1);
            return "Perfect";
        }
        if (distance <= greatThreshold)
        {
            ScoreManager.Instance.AddScore(80);
            ComboManager.Instance.AddCombo(1);
            return "Great";
        }
        if (distance <= goodThreshold)
        {
            ScoreManager.Instance.AddScore(50);
            ComboManager.Instance.AddCombo(1);
            return "Good";
        }
        ComboManager.Instance.AddCombo(0);
        return "Miss";
    }
}
