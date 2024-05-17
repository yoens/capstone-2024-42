using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelectionManager : MonoBehaviour
{
    public static SongSelectionManager Instance { get; private set; }
    public string SelectedSongJsonPath { get; private set; }
    public string SelectedSongID { get; private set; } 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectSong(string jsonPath, string songId)
    {
        SelectedSongJsonPath = jsonPath;
        SelectedSongID = songId;
    }
}
