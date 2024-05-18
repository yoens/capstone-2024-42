using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelSystem : MonoBehaviour {
    private readonly int increaseExperience = 25;

    public void Process()
    {
        int currentlevel = BackendGameData.Instance.UserGameData.level;

        BackendGameData.Instance.UserGameData.userExp += increaseExperience;
    
        if(BackendGameData.Instance.UserGameData.userExp >= BackendChartData.levelChart[currentlevel-1].maxExperience && 
            BackendChartData.levelChart.Count >  currentlevel)
        {
            BackendGameData.Instance.UserGameData.money += BackendChartData.levelChart[currentlevel - 1].rewardGold;
            BackendGameData.Instance.UserGameData.userExp = 0;
            BackendGameData.Instance.UserGameData.level++;
        }

        BackendGameData.Instance.UserDataUpdate();
    }
}
