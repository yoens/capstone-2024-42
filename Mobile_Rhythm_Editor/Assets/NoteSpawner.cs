using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject roundNotePrefab; // 터치판정 노트의 프리팹
    public GameObject lineNotePrefab; // 슬라이드판정 생성할 노트의 프리팹
    public AudioSource audioSource; // 분석할 오디오 소스
    public Transform roundSpawnPoint; // 터치판정 노트를 생성할 위치
    public Transform lineSpawnPoint; // 슬라이드판정 노트를 생성할 위치
    public float threshold = 0.02f; // 에너지 임계값
    private float[] spectrumData = new float[1024]; // 스펙트럼 데이터
    private bool spawnFromLeft = true; // 노트 생성 위치 토글

    void Update()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // 임계값을 초과하는 주파수 대역 감지
        if (spectrumData[0] > threshold)
        {
            SpawnNote();
        }
    }

    void SpawnNote()
    {
        GameObject notePrefab;
        Transform spawnPoint;
        Vector3 direction;

        // 랜덤 값에 따라 노트 종류 결정
        if (Random.value < 0.5) // 터치 판정 노트 생성
        {
            spawnPoint = roundSpawnPoint;
            notePrefab = roundNotePrefab;
            direction = Vector3.zero; // 커지는 노트는 이동하지 않음
        }
        else // 슬라이드 판정 노트 생성
        {
            spawnPoint = lineSpawnPoint;
            notePrefab = lineNotePrefab;
            // 랜덤한 방향을 위한 단위 원에서의 방향 벡터 생성
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
        }

        // 노트 생성
        GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
        NoteMovement noteMovement = note.GetComponent<NoteMovement>();

        // 생성된 노트에 대한 이동 방향 설정
        if (noteMovement != null && direction != Vector3.zero)
        {
        noteMovement.SetDirection(direction);
        }
    }
}