using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class ClearSceneController : MonoBehaviour
{
    public Text scoreText;
    public Text comboText;

    void Start()
    {
        int finalScore = ScoreManager.Instance.Score;
        int maxCombo = ComboManager.Instance.MaxCombo;

        scoreText.text = "Final Score: " + finalScore.ToString();
        comboText.text = "Max Combo: " + maxCombo.ToString();
        SendGameDataToBackEnd();
    }
    void SendGameDataToBackEnd()
    {
        Param param = new Param();
        param.Add("songId", SongSelectionManager.Instance.SelectedSongID);
        param.Add("maxCombo", ComboManager.Instance.MaxCombo);
        param.Add("score", ScoreManager.Instance.Score);
    
        // GameInfo 대신 GameData를 사용하여 데이터 삽입
        BackendReturnObject bro = Backend.GameData.Insert("game_results", param);
        if (bro.IsSuccess())
        {
            Debug.Log("데이터 저장 성공");
        }
        else
        {
            Debug.LogError("데이터 저장 실패: " + bro);
        }
    }
}