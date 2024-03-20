using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawn_bit : MonoBehaviour
{
    public GameObject notePrefab; // 생성할 노트의 프리팹
    public AudioSource audioSource; // 분석할 오디오 소스
    private float[] spectrumData = new float[1024]; // 스펙트럼 데이터
    public float threshold = 0.02f; // 에너지 임계값
    public float spawnRate = 0.5f; // 노트 생성 속도 (초 단위)
    private float nextSpawnTime = 0f; // 다음 노트 생성 시간

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
            if (spectrumData[0] > threshold) // 저주파수 대역의 에너지를 체크
            {
                Instantiate(notePrefab, transform.position, Quaternion.identity); // 노트 생성
                nextSpawnTime = Time.time + 1f / spawnRate; // 다음 생성 시간 설정
            }
        }
    }
}