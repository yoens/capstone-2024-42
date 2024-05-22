using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public static List<string> character_name = new List<string>();
    public static List<string> character_team = new List<string>();
    public static List<string> character_profile = new List<string>();

    void Start()
    {
        Debug.Log(BackendChartData.characterChart.Count);
        for(int i = 0; i < BackendChartData.characterChart.Count; i++)
        {
            character_name.Add(BackendChartData.characterChart[i].characterName);
            character_team.Add(BackendChartData.characterChart[i].characterTeam);
            character_profile.Add(BackendChartData.characterChart[i].characterProfile);
            Debug.Log("i = " + i + " Name : " + character_name[i] + " Team = " + character_team[i]);
        }
    }
}
