using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopBuy : MonoBehaviour
{
    public GameObject panel;

    public int song_id;
    public int original_song_count = 5;

    public TMP_Text Sname;
    public string[] song_name;

    public void touched_song(int id)
    {
        song_id = id;
        Sname.text = song_name[id];
        panel.gameObject.SetActive(true);
    }

    public void touched_buy_song()
    {
        User.song[original_song_count + song_id] = 1;
        panel.gameObject.SetActive(false);
    }
}
