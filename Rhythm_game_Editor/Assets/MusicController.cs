using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider timelineSlider;
    public Button playPauseButton; // Play/Pause 버튼을 위한 참조
    private bool isPlaying = false; // 음악 재생 상태를 추적하는 플래그


    void Start()
    {
        timelineSlider.onValueChanged.AddListener(SetMusicTime);
        timelineSlider.maxValue = audioSource.clip.length;
        playPauseButton.onClick.AddListener(TogglePlayPause); // 버튼 클릭 이벤트에 메서드 연결
        UpdateButtonLabel();
    }

    void Update()
    {
        if (!audioSource.isPlaying) return;
        timelineSlider.value = audioSource.time;
    }

    public void SetMusicTime(float time)
    {
        audioSource.time = time;
    }


    public void TogglePlayPause()
    {
        isPlaying = !isPlaying; // 재생 상태 토글
        if (isPlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
        UpdateButtonLabel();
    }

    void UpdateButtonLabel()
    {
        playPauseButton.GetComponentInChildren<Text>().text = isPlaying ? "Pause" : "Play";
    }
}
