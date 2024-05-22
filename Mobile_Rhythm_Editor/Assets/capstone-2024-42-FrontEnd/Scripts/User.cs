using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User_info
{
    //BackendGameData.Instance.User-.
    public int uid;      // ���� UID
    public string name; // ���� �г���
    public int character = 0;   // ������ ������ ���� ĳ���� id
    public int level = 1;       // ���� ����
    public int exp = 80;        // ���� ����ġ
    public int gold;    // ������ ������ ��ȭ
    public int ranking;    // ���� ��ŷ
    public int score;     // ������ Ŭ������ �� ������ ��
}

 public class User : MonoBehaviour
{
    public static User_info user = new User_info();
    
    public void load_user_data()
    {
        user.uid = 1000;
        user.name = "min";
        user.character = 0;
        user.level = 1;
        user.exp = 80;
        user.gold = 10000;
        user.ranking = 10;
        user.score = 999;
    }
}
