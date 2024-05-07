using UnityEngine;
using BackEnd;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using JetBrains.Annotations;

public class BackendGameData
{
    [System.Serializable]
    public class GameDataLoadEvent : UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private static BackendGameData instance = null;
    public static BackendGameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendGameData();
            }

            return instance;
        }
    }

    private UserGameData userGameData = new UserGameData();
    public UserGameData UserGameData => userGameData;

    private string gameDataRowInDate = string.Empty;

    public void UserDataInsert()
    {
        UserGameData gameData = new UserGameData();

        Param param = gameData.ToParam();

        Backend.GameData.Insert("User", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameDataRowInDate = callback.GetInDate();
                Debug.Log("내 User Info의 indate : " + gameDataRowInDate);
            }
            else
            {
                Debug.LogError("게임 정보 삽입 실패 : " + callback.ToString());
            }
        });
    }

    public void UserDataLoad()
    {
        Backend.GameData.GetMyData("User", new Where(), call_back =>
        {
            if (call_back.IsSuccess())
            {
                Debug.LogError(call_back.ToString());

                try
                {

                    LitJson.JsonData gameDataJson = call_back.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        Debug.Log("데이터가 존재하지 않습니다");
                        return;
                    }
                    else
                    {
                        UserGameData.Json_write(gameDataJson);

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        });
    }
    public void UserDataUpdate(UnityAction action=null)
    {
        if(userGameData == null)
        {
            Debug.Log("Insert or Load 먼저 필요함");
            return;
        }

        Param param = userGameData.ToParam();

        if(string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.LogError("유저의 inDate 정보가 X");
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정 요청");

            Backend.GameData.UpdateV2("User", gameDataRowInDate, Backend.UserInDate, param, callback =>
            {
                if(callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 데이터 수정 성공 : {callback}");

                    action?.Invoke();

//                    BackendGameData.Instance.UserDataUpdate();
                }
                else
                {
                    Debug.Log($"게임 정보 데이터 수정 실패 : {callback}");
                }
            });
        }
    }
}
