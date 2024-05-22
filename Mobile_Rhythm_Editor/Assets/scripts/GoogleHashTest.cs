using UnityEngine;
using BackEnd;
using TMPro; // TextMeshPro 네임스페이스 추가

public class GoogleHashTest : MonoBehaviour
{
    public TMP_InputField inputField; // TMP_InputField 사용

    void BackendCallback(BackendReturnObject bro)
    {
        if (bro.IsSuccess())
        {
            Debug.Log("뒤끝 초기화 성공");
            GetGoogleHash(); // 뒤끝 초기화 성공 시 해시 값 얻기
        }
        else
        {
            Debug.LogError("뒤끝 초기화 실패: " + bro);
        }
    }

    public void GetGoogleHash()
    {
        string googleHashKey = Backend.Utils.GetGoogleHash();
        if (!string.IsNullOrEmpty(googleHashKey))
        {
            Debug.Log("구글 해시 키: " + googleHashKey);
            if (inputField != null)
            {
                inputField.text = googleHashKey;
            }
        }
        else
        {
            Debug.LogError("구글 해시 키를 가져올 수 없습니다.");
        }
    }
}
