using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class ClearSceneController : MonoBehaviour
{
    public Text scoreText;
    public Text comboText;
    public bool clear = true;
    public int characterID;
    public int characterLevel;
    public int characterExp;
    public int count;

    void Start()
    {
        int finalScore = ScoreManager.Instance.Score;
        int maxCombo = ComboManager.Instance.MaxCombo;
        int upsongid;
        characterExp = finalScore / 1000;

        scoreText.text = "Final Score: " + finalScore.ToString();
        comboText.text = "Max Combo: " + maxCombo.ToString();

        PlayerSongGameData songdata = new PlayerSongGameData();
        SongSelectionManager ssman = new SongSelectionManager();
        upsongid = ssman.ssongid;
        songdata.songID =  upsongid;
        songdata.score = finalScore;
        songdata.combo = maxCombo;
        songdata.clear = true;

        BackendGameData.Instance.PlayerSongDataUpdate(upsongid);
    }
}