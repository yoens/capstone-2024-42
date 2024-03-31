using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject yellowNotePrefab; // 노란 노트 프리팹
    public GameObject greenNotePrefab; // 초록 노트 프리팹
    public Transform yellowSpawnPoint;//노란 노트 생성지점
    public Transform greenSpawnPoint;//초록 노트 생성지점

    // 노란 노트를 생성하는 메소드
    public void SpawnYellowNote()
    {
        GameObject notePrefab;
        Transform spawnPoint;
        spawnPoint = yellowSpawnPoint;
        notePrefab = yellowNotePrefab;
        Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);        
    }

    // 초록 노트를 생성하는 메소드
    public void SpawnGreenNote()
    {
        GameObject notePrefab;
        Transform spawnPoint;
        spawnPoint = greenSpawnPoint;
        notePrefab = greenNotePrefab;
        Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);    
    }
}