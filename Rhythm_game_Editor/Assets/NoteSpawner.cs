using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject leftNotePrefab; // 왼쪽에서 생성할 노트의 프리팹
    public GameObject rightNotePrefab; // 오른쪽에서 생성할 노트의 프리팹
    public AudioSource audioSource; // 분석할 오디오 소스
    public Transform leftSpawnPoint; // 왼쪽에서 노트를 생성할 위치
    public Transform rightSpawnPoint; // 오른쪽에서 노트를 생성할 위치
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
        // 현재 노트를 생성할 위치와 사용할 프리팹을 결정
        Transform spawnPoint = spawnFromLeft ? leftSpawnPoint : rightSpawnPoint;
        GameObject notePrefab = spawnFromLeft ? leftNotePrefab : rightNotePrefab;

        // 노트 생성
        GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
        
        // 생성된 노트에 대한 추가 설정 (예: 이동 방향 설정)
        NoteMovement noteMovement = note.GetComponent<NoteMovement>();
        if (noteMovement != null)
        {
            Vector3 direction = spawnFromLeft ? Vector3.right : Vector3.left;
            noteMovement.SetDirection(direction);
        }

        // 다음 노트 생성 위치 변경
        spawnFromLeft = !spawnFromLeft;
    }
}