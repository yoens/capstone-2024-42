using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab; // Inspector에서 할당할 노트 프리팹
    public AudioSource music; // 음악을 재생할 AudioSource 컴포넌트 (Inspector에서 할당)

    private void Start()
    {
        StartCoroutine(SpawnNoteRoutine());
    }

    private IEnumerator SpawnNoteRoutine()
    {
        // 음악이 재생되기 시작할 때까지 대기
        yield return new WaitUntil(() => music.isPlaying);

        while (music.isPlaying) // 음악이 재생되는 동안 노트 생성
        {
            SpawnNote();
            yield return new WaitForSeconds(1.0f); // 1초마다 노트 생성
        }
    }

    void SpawnNote()
    {
        Instantiate(notePrefab, transform.position, Quaternion.identity);
    }
}