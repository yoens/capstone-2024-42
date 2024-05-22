using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main_Scene_Manager : MonoBehaviour
{
    public TMP_Text Text_user_name;
    public TMP_Text Text_user_level;
    public TMP_Text Text_gold;
    public Image    Image_character;

    public Sprite[] character_image_sprite;
    
    public Slider user_exp_bar;
    public GameObject gauge;

    void Start()
    {
        //Text_user_name.text = User.user.name.ToString();
        //Text_user_level.text = User.user.level.ToString();
        //Text_gold.text = User.user.gold.ToString();
        Image_character.sprite = character_image_sprite[User.user.character];

        // ���� ����ġ �� Ȱ�� ��ũ��Ʈ
        int exp = User.user.exp;
        if (exp == 0) gauge.gameObject.SetActive(false);
        else
        {
            gauge.gameObject.SetActive(true);
            user_exp_bar.value = (float)exp / (float)BackendChartData.levelChart[User.user.level - 1].maxExperience;
        }
    }

    void Update()
    {

    }
}
