using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public ObjectPoolManager objectPoolManager;
    public Transform[] spawnPoints;
    public AudioManager audioManager;
    public NoteManager noteManager;
    public Transform centerPoint;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) {
            SpawnNote("Note_Y", spawnPoints[0].position);
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            SpawnNote("Note_G", spawnPoints[1].position);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SpawnNote("Note_R", spawnPoints[2].position);
        }
    }

    void SpawnNote(string type, Vector3 spawnPosition)
    {
        GameObject note = objectPoolManager.GetObject(type);
        if (note == null) return;

        note.transform.position = spawnPosition; // 스폰 위치 설정
        note.SetActive(true); // 노트 활성화

        CircularMotion motionScript = note.GetComponent<CircularMotion>();
        if (motionScript != null)
        {
            motionScript.centerPoint = centerPoint; // 중심점 설정
            motionScript.InitializeMotion(spawnPosition); // 원운동 초기화
        }

        float musicTime = audioManager.GetMusicTime();
        noteManager.AddNote(musicTime, type);
    }
}