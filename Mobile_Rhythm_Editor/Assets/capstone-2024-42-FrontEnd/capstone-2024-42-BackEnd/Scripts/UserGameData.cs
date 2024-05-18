using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;

public class UserGameData : MonoBehaviour
{
    public string nickname = Backend.UserNickName;
    public string ownerIndate = Backend.UserInDate;
    public int level;
    public int userExp;
    public int userScore;
    public int dailyScore;
    public int money;
    public int selectCharacter_num;

    public UserGameData()
    {

    }
    private void Reset()
    {
    }

    public Param ToParam()
    {
        Param param = new Param();
        param.Add("Level", level);
        param.Add("money", money);
        param.Add("SelectCharacter", selectCharacter_num);
        param.Add("UserExp", userExp);
        param.Add("UserScore", userScore);
        param.Add("DailyScore", dailyScore);

        return param;
    }

    public void Json_write(LitJson.JsonData gameDataJson)
    {
        ownerIndate = gameDataJson[0]["InDate"].ToString();
        level = int.Parse(gameDataJson[0]["level"].ToString());
        money = int.Parse(gameDataJson[0]["Money"].ToString());
        userExp = int.Parse(gameDataJson[0]["UserExp"].ToString());
        userScore = int.Parse(gameDataJson[0]["UserScore"].ToString());
        dailyScore = int.Parse(gameDataJson[0]["DailyScore"].ToString());
        selectCharacter_num = int.Parse(gameDataJson[0]["SelectCharacter"].ToString());

    }
}
