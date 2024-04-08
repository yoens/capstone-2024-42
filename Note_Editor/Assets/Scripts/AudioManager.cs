using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public TMP_Text musicTimeText;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // 음악이 재생 중일 때만 시간을 업데이트
        if (audioSource.isPlaying)
        {
            UpdateMusicTimeUI(audioSource.time);
        }
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public float GetMusicTime()
    {
        return audioSource.time;
    }
    private void UpdateMusicTimeUI(float currentTime)
    {
        // 현재 시간을 mm:ss 형태로 표시
        int minutes = (int)currentTime / 60;
        int seconds = (int)currentTime % 60;
        musicTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 음악 시간을 설정하는 메서드 (예: 사용자가 드래그할 때 호출됨)
    public void SetMusicTime(float time)
    {
        audioSource.time = time;
        UpdateMusicTimeUI(time); // UI도 즉시 업데이트
    }
    public void RewindMusicBy(float seconds)
    {
        audioSource.time = Mathf.Max(0, audioSource.time - seconds); // 음악 시간을 되감기
    }
}
