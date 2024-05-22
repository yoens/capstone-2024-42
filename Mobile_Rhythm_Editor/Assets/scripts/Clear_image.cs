using UnityEngine;
using UnityEngine.UI; // UI 네임스페이스 추가

public class ClearImage : MonoBehaviour
{
    public Image clearImage; // Image 컴포넌트 참조
    public Sprite char1Image;
    public Sprite char2Image;
    int choiceChar;

    void Start()
    {
        choiceChar = User.user.character; // 캐릭터 선택 정보를 가져옵니다.
        ChangeClearImage(choiceChar); // 이미지 변경 메서드 호출
    }

    public void ChangeClearImage(int choice)
    {
        if (choice == 0)
        {
            clearImage.sprite = char1Image; // 첫 번째 캐릭터 이미지로 변경
        }
        else if (choice == 1)
        {
            clearImage.sprite = char2Image; // 두 번째 캐릭터 이미지로 변경
        }
    }
}
