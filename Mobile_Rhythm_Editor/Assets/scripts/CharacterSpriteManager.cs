using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour
{
    public SpriteRenderer characterRenderer;
    public Sprite missSprite;
    public Sprite goodSprite;
    public Sprite greatSprite;
    public Sprite perfectSprite;

    public void ChangeSprite(string judgment)
    {
        switch (judgment)
        {
            case "Miss":
                characterRenderer.sprite = missSprite;
                break;
            case "Good":
                characterRenderer.sprite = goodSprite;
                break;
            case "Great":
                characterRenderer.sprite = greatSprite;
                break;
            case "Perfect":
                characterRenderer.sprite = perfectSprite;
                break;
        }
    }
}
