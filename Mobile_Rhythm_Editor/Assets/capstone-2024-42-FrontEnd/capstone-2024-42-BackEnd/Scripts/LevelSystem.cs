using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelSystem : MonoBehaviour {
    private readonly int increaseExperience = 25;

    public void User_Process()
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

    public void Character_Process(int character_ID)
    {
        int currentlevel = 0;
        int character_num = -1;

        for (int i = 0; i < Constants.CHARACTER_NUMBER; i++)
        {
            if (BackendGameData.Instance.PlayerCharacterGameData[i].characterID == character_ID)
            {
                currentlevel = BackendGameData.Instance.PlayerCharacterGameData[i].characterLevel;
                character_num = i;
            }
        }

        if(character_num < 0)
        {
            Debug.Log("레벨 - 캐릭터 ID 값이 잘못되었습니다.");
        }

        BackendGameData.Instance.PlayerCharacterGameData[character_num].characterExp += increaseExperience;

        if (BackendGameData.Instance.PlayerCharacterGameData[character_num].characterExp >= BackendChartData.levelChart[currentlevel - 1].maxExperience &&
            BackendChartData.levelChart.Count > currentlevel)
        {
            BackendGameData.Instance.UserGameData.money += BackendChartData.levelChart[currentlevel - 1].rewardGold;
            BackendGameData.Instance.PlayerCharacterGameData[character_num].characterExp = 0;
            BackendGameData.Instance.PlayerCharacterGameData[character_num].characterLevel++;
        }

        BackendGameData.Instance.UserDataUpdate();

    }
}
