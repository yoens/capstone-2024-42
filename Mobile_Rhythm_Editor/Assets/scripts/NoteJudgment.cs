using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NoteJudgment : MonoBehaviour
{
    public Transform judgmentLine; // 판정선 위치
    public float perfectThreshold = 0.05f; // Perfect 판정 거리
    public float greatThreshold = 0.1f; // Great 판정 거리
    public float goodThreshold = 0.15f; // Good 판정 거리
    public float missThreshold = 0.2f; // Miss 판정 거리

    private List<GameObject> notesInPlay = new List<GameObject>(); // 현재 활성화된 노트들의 리스트

    void Start()
    {
        // Start()에서 버튼 리스너를 추가하는 코드는 제거됨
    }

    void Update()
    {
        UpdateNotesInPlay();
        CheckMissedNotes();
    }

    void UpdateNotesInPlay()
    {
        // 현재 활성화된 노트들을 업데이트
        notesInPlay.Clear();
        notesInPlay.AddRange(GameObject.FindGameObjectsWithTag("Note_Y"));
        notesInPlay.AddRange(GameObject.FindGameObjectsWithTag("Note_G"));
        notesInPlay.AddRange(GameObject.FindGameObjectsWithTag("Note_R"));
    }

    void CheckMissedNotes()
    {
        // 판정선을 넘어서 미스가 된 노트를 체크
        for (int i = notesInPlay.Count - 1; i >= 0; i--)
        {
            var note = notesInPlay[i];
            if (note.transform.position.y < judgmentLine.position.y - missThreshold)
            {
                Debug.Log("Miss by distance: " + note.tag);
                Destroy(note);
                notesInPlay.RemoveAt(i);
            }
        }
    }

    // 버튼 클릭 이벤트에 의해 호출될 새로운 공개 메소드들
    public void JudgeClosestNote_Y()
    {
        JudgeClosestNote("Note_Y");
    }

    public void JudgeClosestNote_G()
    {
        JudgeClosestNote("Note_G");
    }

    public void JudgeClosestNote_R()
    {
        JudgeClosestNote("Note_R");
    }

    // 각 노트 타입에 대한 가장 가까운 노트를 판정하는 메소드
    private void JudgeClosestNote(string noteType)
    {
        // 버튼 클릭 시 해당 타입의 노트 중 가장 판정선에 가까운 노트를 찾아 판정
        var notes = notesInPlay.Where(note => note.tag == noteType).ToList();
        GameObject closestNote = null;
        float minDistance = float.MaxValue;

        foreach (GameObject note in notes)
        {
            float distance = Mathf.Abs(note.transform.position.y - judgmentLine.position.y);
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

    // 거리에 따라 판정 결과를 결정하는 메소드
    private string DetermineJudgment(float distance)
    {
        if (distance <= perfectThreshold)
            return "Perfect";
        if (distance <= greatThreshold)
            return "Great";
        if (distance <= goodThreshold)
            return "Good";

        return "Miss";
    }
}
