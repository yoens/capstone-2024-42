using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NoteJudgment : MonoBehaviour
{   public Transform judgmentLine_Y;
    public Transform judgmentLine_G;
    public Transform judgmentLine_R;
    public GameObject effectPrefab_Y;
    public GameObject effectPrefab_G;
    public GameObject effectPrefab_R;

    public float perfectThreshold = 0.1f;
    public float greatThreshold = 0.2f;
    public float goodThreshold = 0.3f;
    public CharacterSpriteManager spriteManager;

    private List<GameObject> notesInPlay = new List<GameObject>();

    private void Awake()
    {
        spriteManager = FindObjectOfType<CharacterSpriteManager>(); // 씬에서 스프라이트 매니저 찾기
    }

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
        for (int i = notesInPlay.Count - 1; i >= 0; i--)
        {
            var note = notesInPlay[i];
            Transform specificJudgmentLine = GetJudgmentLine(note.tag);

            if (specificJudgmentLine != null && note.transform.position.y < specificJudgmentLine.position.y)
            {
                Debug.Log("Miss: " + note.tag);
                PerformMissAction();
                Destroy(note);
                notesInPlay.RemoveAt(i);
            }
        }
    }

    public void JudgeClosestNote_Y() { JudgeClosestNote("Note_Y", judgmentLine_Y); }
    public void JudgeClosestNote_G() { JudgeClosestNote("Note_G", judgmentLine_G); }
    public void JudgeClosestNote_R() { JudgeClosestNote("Note_R", judgmentLine_R); }

    private Transform GetJudgmentLine(string noteType)
    {
        switch (noteType)
        {
            case "Note_Y": return judgmentLine_Y;
            case "Note_G": return judgmentLine_G;
            case "Note_R": return judgmentLine_R;
            default: return null;
        }
    }

    private void JudgeClosestNote(string noteType, Transform specificJudgmentLine)
    {
        var notes = notesInPlay.Where(note => note.tag == noteType && note.transform.position.y >= specificJudgmentLine.position.y - goodThreshold).ToList();
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

        if (closestNote != null)
        {
            string judgment = DetermineJudgment(closestNote, specificJudgmentLine);
            if (judgment != null)
            {
                Debug.Log(noteType + " " + judgment);
                GameObject effectPrefab = GetEffectPrefab(noteType);
                if (effectPrefab != null)
                {
                    GameObject effect = Instantiate(effectPrefab, specificJudgmentLine.position, Quaternion.Euler(0, 0, 315));
                    Destroy(effect, 1f);
                    Debug.Log("Effect created for " + noteType);
                }
                Destroy(closestNote);
                notesInPlay.Remove(closestNote);
            }
        }
    }

    private GameObject GetEffectPrefab(string noteType)
    {
        switch (noteType)
        {
            case "Note_Y": return effectPrefab_Y;
            case "Note_G": return effectPrefab_G;
            case "Note_R": return effectPrefab_R;
            default: return null;
        }
    }

    private string DetermineJudgment(GameObject note, Transform specificJudgmentLine)
    {
        float distance = Mathf.Abs(note.transform.position.y - specificJudgmentLine.position.y);
        string result;
        if (distance <= perfectThreshold)
        {
            PerformAction(100, 1, 1);
            result = "Perfect";
        }
        else if (distance <= greatThreshold)
        {
            PerformAction(80, 1, 1);
            result = "Great";
        }
        else if (distance <= goodThreshold)
        {
            PerformAction(50, 1, 1);
            result = "Good";
        }
        else if (note.transform.position.y < specificJudgmentLine.position.y)
        {
            PerformMissAction();
            result = "Miss";
        }
        else
        {
            return null; // 판정선을 넘기 전이면 판정하지 않음
        }

        if (spriteManager != null)
        {
            spriteManager.ChangeSprite(result);
        }
        return result;
    }

    private void PerformAction(int score, int combo, int points)
    {
        if (ScoreManager.Instance != null) ScoreManager.Instance.AddScore(score);
        if (ComboManager.Instance != null) ComboManager.Instance.AddCombo(combo);
        if (LifeManager.Instance != null) LifeManager.Instance.Plus(points);
    }

    private void PerformMissAction()
    {
        Debug.Log("Miss Action Triggered");
        if (spriteManager != null)
        {
            spriteManager.ChangeSprite("Miss");
        }
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(0);
        if (ComboManager.Instance != null)
            ComboManager.Instance.ResetCombo();
        if (LifeManager.Instance != null)
            LifeManager.Instance.Minus(3);
    }
}
