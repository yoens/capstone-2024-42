using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class User : MonoBehaviour
{
    public TMP_Text Text_user_name;
    public TMP_Text Text_user_level;
    public TMP_Text Text_gold;
    //BackendGameData.Instance.User-.
    public static int uid = 1000;               // 유저 UID
    public static string user_name = "min";     // 유저가 설정한 이름
    public static int character = 0;            // 유저가 선택한 현재 캐릭터 id
    public static int user_level = 5;           // 유저의 레벨
    public static int exp = 50;                        // 유저 경험치
    public static int gold = 10000;                    // 유저가 보유한 재화
    public static int ranking = 10;             // 유저 랭킹
    public static int score = 999;              // 유저가 클리어한 곡 점수의 합

    public static int[] song = { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 }; // 유저가 보유한 곡 (1이 보유)
    public int[] clear_song = { 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 }; // 해당 곡의 클리어 유무 (1이 클리어)
    public int[] score_song = { 999, 0, 1999, 0, 2999, 0, 0, 0, 0, 0 }; // 해당 곡에서 얻은 최고 점수

    // Start is called before the first frame update
    void Start()
    {
        Text_user_level.text = user_level.ToString();
        Text_gold.text = gold.ToString();
        Text_user_name.text = user_name.ToString();
    }

    public void touch_character_select_button(int a)
    {
        character = a;
    }
}