using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public float time; // 노트가 나타날 시간
    public string type; // 노트의 타입
}

[System.Serializable]
public class NoteDataCollection
{
    public NoteData[] Notes;
}

public class NoteSpawner : MonoBehaviour
{
    public AudioSource audioSource; // 게임의 배경 음악 오디오 소스
    public GameObject[] notePrefabs; // 노트 프리팹 배열
    private List<NoteData> notesToSpawn; // 스폰할 노트 데이터 리스트
    public string jsonFilePath = "Assets/Guilty Bear_bpm120_Am.json";  // JSON 파일 경로
    private Dictionary<string, Transform> spawnPointMap; // 노트 타입별 스폰 위치
    public Transform[] spawnPoints; // Unity 인스펙터에서 설정할 스폰 포인트 배열
    // CircularMotion을 위한 변수
    public Transform centerPoint; // 원의 중심점
    public float radius = 1.0f; // 원의 반지름
    public float speed = 60f; // 회전 속도

    // NoteMovement를 위한 변수
    public float approachSpeed = 5f; // 접근 속도
    public float startZ = 30f; // 시작 Z 위치
    public float endZ = 0f; // 종료 Z 위치


    void Start()
    {
        InitializeSpawnPoints();
        LoadNotesData();
        audioSource.Play();
    }

    void Update()
    {
        if (notesToSpawn.Count > 0 && audioSource.isPlaying)
        {
            if (audioSource.time >= notesToSpawn[0].time)
            {
                SpawnNote(notesToSpawn[0]);
                notesToSpawn.RemoveAt(0);
            }
        }
    }

    void LoadNotesData()
    {
        string jsonText = File.ReadAllText(jsonFilePath);
        NoteDataCollection loadedData = JsonUtility.FromJson<NoteDataCollection>(jsonText);
        if (loadedData != null && loadedData.Notes != null)
        {
            notesToSpawn = new List<NoteData>(loadedData.Notes);
        }
        else
        {
            Debug.LogError("Failed to load notes data or notes are null.");
        }
    }

    void InitializeSpawnPoints()
    {
        spawnPointMap = new Dictionary<string, Transform>();
        for (int i = 0; i <= 7; i++)
        {
            spawnPointMap["Note_" + i.ToString()] = spawnPoints[i];
        }
        spawnPointMap["Note_Y"] = spawnPoints[8];
        spawnPointMap["Note_G"] = spawnPoints[9];
        spawnPointMap["Note_R"] = spawnPoints[10];
    }

    void SpawnNote(NoteData noteData)
    {
        GameObject prefab = notePrefabs.FirstOrDefault(p => p.name == noteData.type);
        if (prefab != null)
        {
            Transform spawnLocation = spawnPointMap[noteData.type];
            GameObject spawnedNote = Instantiate(prefab, spawnLocation.position, Quaternion.identity);
            
            if (noteData.type == "Note_Y" || noteData.type == "Note_G" || noteData.type == "Note_R")
            {
                CircularMotion motion = spawnedNote.GetComponent<CircularMotion>();
                if (motion)
                {
                    motion.centerPoint = centerPoint;
                    motion.Initialize(spawnLocation.position); // 스폰 위치를 기준으로 초기화
                }
            }
            else if (noteData.type.StartsWith("Note_"))
            {
                NoteMovement movement = spawnedNote.GetComponent<NoteMovement>();
                if (movement)
                {
                    movement.approachSpeed = approachSpeed;
                    movement.startZ = startZ;
                    movement.endZ = endZ;
                }
            }
        }
        else
        {
            Debug.LogError("Prefab not found for type: " + noteData.type);
        }
    }
}
