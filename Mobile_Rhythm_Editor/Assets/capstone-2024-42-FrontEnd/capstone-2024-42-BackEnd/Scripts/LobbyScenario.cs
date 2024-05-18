using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScenario : MonoBehaviour
{
    [SerializeField]
    private UserInfo user;

    private void Awake()
    {
        user.GetUserInfoFromBackend();
    }

    private void Start()
    {
        BackendGameData.Instance.UserDataLoad();

        for (int i = 0; i < BackendChartData.songChart.Count; i++)
        {
            BackendGameData.Instance.PlayerSongDataLoad(i);
        }

        for (int i = 0; i < BackendChartData.characterChart.Count; i++)
        {
            BackendGameData.Instance.PlayerCharacterDataLoad(i);
        }

        //        BackendGameData.Instance.UserGameData.nickname;

//        BackendGameData.Instance.PlayerCharacterGameData.Count();

    }
}
