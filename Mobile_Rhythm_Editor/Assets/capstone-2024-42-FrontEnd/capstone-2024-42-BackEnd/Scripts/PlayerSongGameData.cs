using BackEnd;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerSongGameData : MonoBehaviour
{
    public string ownerIndate = Backend.UserInDate;
    public int songID;
    public int score;
    public int combo;
    public bool clear;

    public PlayerSongGameData()
    {

    }

    private void Reset()
    {
    }

    public Param ToParam()
    {
        Param param = new Param();
        param.Add("SongID", songID);
        param.Add("Score", score);
        param.Add("Combo", combo);
        param.Add("Clear", clear);

        return param;
    }

    public void Json_write(LitJson.JsonData gameDataJson)
    {   
        ownerIndate = gameDataJson[0]["InDate"].ToString();
        songID = int.Parse(gameDataJson[0]["SongID"].ToString());
        score = int.Parse(gameDataJson[0]["Score"].ToString());
        combo = int.Parse(gameDataJson[0]["Combo"].ToString());
        clear = bool.Parse(gameDataJson[0]["Clear"].ToString());
    }
}