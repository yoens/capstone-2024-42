using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Exp_Slider : MonoBehaviour
{
    public Slider user_exp_bar;
    public GameObject gauge;
    public float max_exp = 100;

    void Start()
    {
        if (User.exp == 0) gauge.gameObject.SetActive(false);
        else
        {
            gauge.gameObject.SetActive(true);
            user_exp_bar.value = (float)User.exp / (float)max_exp;
        }
    }
}
