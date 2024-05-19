using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SongSelectionManager : MonoBehaviour
{
    public static SongSelectionManager Instance { get; private set; }
    public AudioSource audioSource; // AudioSource 컴포넌트 참조를 위한 필드

    public string SelectedSongJsonPath { get; private set; }
    public string SelectedSongID { get; private set; } 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환시 객체 유지
        }
        else
        {
            Destroy(gameObject);
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
        }
    }

    // 곡 ID를 기반으로 곡을 선택하고 JSON 경로를 설정
    public void SelectSong(string songId)
    {
        SelectedSongID = songId;
        SelectedSongJsonPath = "JSONs/" + songId + ".json";  // Resources/JSONs 폴더 내에 저장된다고 가정

        // 음악 파일 로드 및 재생
        AudioClip clip = Resources.Load<AudioClip>("Songs/" + songId);
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("음악 파일을 찾을 수 없습니다: Songs/" + songId);
        }

        // 게임 씬 로드
        LoadGameScene();
    }

    public void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(SelectedSongID))
        {
            SceneManager.LoadScene("InGame");
        }
        else
        {
            Debug.LogError("노래가 선택되지 않았습니다.");
        }
    }
}
