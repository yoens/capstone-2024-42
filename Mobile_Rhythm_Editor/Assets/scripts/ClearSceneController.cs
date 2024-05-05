using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClearSceneController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    void Start()
    {
        int finalScore = ScoreManager.Instance.Score;
        int maxCombo = ComboManager.Instance.MaxCombo;

        scoreText.text = "Final Score: " + finalScore.ToString();
        comboText.text = "Max Combo: " + maxCombo.ToString();
    }
}