using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongList : MonoBehaviour
{
    public int Song_Count = 10;
    public GameObject [] Song = new GameObject [10];
   
    void Start() // 곡 선택 창에서 자신이 보유한 곡들만 보여주는 기능
    {
        for (int i = 0; i < Song_Count ; i++)
        {
            if (User.song[i] == 0)
            {
                Song[i].gameObject.SetActive(false);
            }
        }
    }
}
