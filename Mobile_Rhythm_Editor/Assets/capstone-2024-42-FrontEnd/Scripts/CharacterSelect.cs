using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public GameObject panel;

    public TMP_Text Cname;
    public TMP_Text Cteam;
    public TMP_Text Cprofile;

    public Image image;
    public Sprite[] sprites;

    public int characterId;

    public void cliked_character(int id)
    {
        Debug.Log("clicked character ");
        Debug.Log(Character.character_name[id]);
        characterId = id;
        Cname.text = Character.character_name[id];
        Cteam.text = Character.character_team[id];
        Cprofile.text = Character.character_profile[id];
        image.sprite = sprites[id];
        panel.gameObject.SetActive(true);
    }

    public void touch_character_select_button()
    {
        User.user.character = characterId;
    }
}

