using UnityEngine;
using UnityEngine.UI;

public class NoteJudgment : MonoBehaviour
{
    public Transform judgmentLine; // 판정선의 위치 Transform
    public float perfectThresholdX = 0.05f; // X축 완벽한 판정 거리
    public float perfectThresholdY = 0.05f; // Y축 완벽한 판정 거리
    public float greatThresholdX = 0.1f; // X축 Great 판정 거리
    public float greatThresholdY = 0.1f; // Y축 Great 판정 거리
    public float goodThresholdX = 0.15f; // X축 Good 판정 거리
    public float goodThresholdY = 0.15f; // Y축 Good 판정 거리

    public Button yellowButton;
    public Button greenButton;
    public Button redButton;

    void Start()
    {
        yellowButton.onClick.AddListener(() => JudgeNote("Note_Y"));
        greenButton.onClick.AddListener(() => JudgeNote("Note_G"));
        redButton.onClick.AddListener(() => JudgeNote("Note_R"));
    }

    void JudgeNote(string noteType)
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag(noteType);
        foreach (GameObject note in notes)
        {
            string judgment = DetermineJudgment(note.transform);

            if (judgment != "Miss")
            {
                Debug.Log(noteType + " " + judgment);
                Destroy(note);
            }
        }
    }

    string DetermineJudgment(Transform noteTransform)
    {
        float distanceX = Mathf.Abs(noteTransform.position.x - judgmentLine.position.x);
        float distanceY = Mathf.Abs(noteTransform.position.y - judgmentLine.position.y);

        if (distanceX <= perfectThresholdX && distanceY <= perfectThresholdY)
            return "Perfect";
        if (distanceX <= greatThresholdX && distanceY <= greatThresholdY)
            return "Great";
        if (distanceX <= goodThresholdX && distanceY <= goodThresholdY)
            return "Good";

        return "Miss";
    }
}
