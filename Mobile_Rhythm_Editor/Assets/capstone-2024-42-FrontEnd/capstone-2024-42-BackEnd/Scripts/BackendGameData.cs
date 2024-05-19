using UnityEngine;
using BackEnd;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using JetBrains.Annotations;
using UnityEngine.TextCore.Text;

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
    private List<PlayerSongGameData> playerSongGameData = new List<PlayerSongGameData>();
    private List<PlayerCharacterGameData> playerCharacterGameData = new List<PlayerCharacterGameData>();

    public UserGameData UserGameData => userGameData;
    public List<PlayerSongGameData> PlayerSongGameData => playerSongGameData;
    public List<PlayerCharacterGameData> PlayerCharacterGameData => playerCharacterGameData;

    private string UserDataRowInDate = string.Empty;
    private string SongDataRowInDate = string.Empty;
    private string CharacterDataRowInDate = string.Empty;

    public void UserDataInsert()
    {
        UserGameData gameData = new UserGameData();

        Param param = gameData.ToParam();

        Backend.GameData.Insert("User", param, callback =>
        {
            if (callback.IsSuccess())
            {
                UserDataRowInDate = callback.GetInDate();
                Debug.Log("내 User Info의 indate : " + UserDataRowInDate);
            }
            else
            {
                Debug.LogError("유저 게임 정보 삽입 실패 : " + callback.ToString());
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

        if(string.IsNullOrEmpty(UserDataRowInDate))
        {
            Debug.LogError("유저의 inDate 정보가 X");
        }
        else
        {
            Debug.Log($"{UserDataRowInDate}의 게임 정보 데이터 수정 요청");

            Backend.GameData.UpdateV2("User", UserDataRowInDate, Backend.UserInDate, param, callback =>
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

    public void PlayerSongDataInsert(int songID)
    {
        PlayerSongGameData gameData = new PlayerSongGameData();
        gameData.songID = songID;
        Param param = gameData.ToParam();

        Backend.GameData.Insert("PlayerSong", param, callback =>
        {
            if (callback.IsSuccess())
            {
                SongDataRowInDate = callback.GetInDate();
                Debug.Log("내 Player Song Info의 indate : " + SongDataRowInDate);
            }
            else
            {
                Debug.LogError("곡 게임 정보 삽입 실패 : " + callback.ToString());
            }
        });
    }

    public void PlayerSongDataLoad(int songID)
    {
        Where where = new Where();
        Where.Equals("SongID", songID);

        Backend.GameData.GetMyData("PlayerSong", where, call_back =>
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
                        playerSongGameData[songID].Json_write(gameDataJson);

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

    public void PlayerSongDataUpdate(int songID, UnityAction action = null)
    {
        if (playerSongGameData == null)
        {
            Debug.Log("Insert or Load 먼저 필요함");
            return;
        }

        Param param = playerSongGameData[songID].ToParam();

        if (string.IsNullOrEmpty(SongDataRowInDate))
        {
            Debug.LogError("유저 곡의 inDate 정보가 X");
        }
        else
        {
            Debug.Log($"{SongDataRowInDate}의 게임 정보 데이터 수정 요청");

            Backend.GameData.UpdateV2("PlayerSong", SongDataRowInDate, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 데이터 수정 성공 : {callback}");

                    action?.Invoke();

                    //                    BackendGameData.Instance.UserDataUpdate();
                }
                else
                {
                    Debug.Log($"곡 게임 정보 데이터 수정 실패 : {callback}");
                }
            });
        }
    }

    public void PlayerCharacterDataInsert(int CharacterID)
    {
        PlayerCharacterGameData gameData = new PlayerCharacterGameData();
        gameData.characterID = CharacterID;
        Param param = gameData.ToParam();

        Backend.GameData.Insert("PlayerCharacter", param, callback =>
        {
            if (callback.IsSuccess())
            {
                CharacterDataRowInDate = callback.GetInDate();
                Debug.Log("내 Player Character Info의 indate : " + CharacterDataRowInDate);
            }
            else
            {
                Debug.LogError("캐릭터 게임 정보 삽입 실패 : " + callback.ToString());
            }
        });
    }
 
    public void PlayerCharacterDataLoad(int characterID)
    {
        Where where = new Where();
        Where.Equals("CharacterID", characterID);

        Backend.GameData.GetMyData("PlayerCharacter", where, call_back =>
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
                        playerCharacterGameData[characterID].Json_write(gameDataJson);

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

    public void PlayerCharacterDataUpdate(int characterID, UnityAction action = null)
    {
        if (playerCharacterGameData == null)
        {
            Debug.Log("Insert or Load 먼저 필요함");
            return;
        }

        Param param = playerCharacterGameData[characterID].ToParam();

        if (string.IsNullOrEmpty(CharacterDataRowInDate))
        {
            Debug.LogError("유저 곡의 inDate 정보가 X");
        }
        else
        {
            Debug.Log($"{CharacterDataRowInDate}의 게임 정보 데이터 수정 요청");

            Backend.GameData.UpdateV2("PlayerCharacter", CharacterDataRowInDate, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
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