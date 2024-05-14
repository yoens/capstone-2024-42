using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Uer_Info_Manager : MonoBehaviour
{
    public TMP_Text Text_uid;
    public TMP_Text Text_user_name;
    public TMP_Text Text_user_level;
    public TMP_Text Text_user_ranking;
    public TMP_Text Text_user_score;
    public TMP_Text Text_gold;

    void Start()
    {
        Text_uid.text = User.uid.ToString();
        Text_user_name.text = User.user_name.ToString();
        Text_user_level.text = User.user_level.ToString();
        Text_user_ranking.text = User.ranking.ToString();
        Text_user_score.text = User.score.ToString();
    }
}
