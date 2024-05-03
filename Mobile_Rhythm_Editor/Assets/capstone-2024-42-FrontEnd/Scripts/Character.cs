using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Sprite[] character_image_sprite;
    public Image character_image;
    // Start is called before the first frame update
    void Start()
    {
        character_image.sprite = character_image_sprite[User.character];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
