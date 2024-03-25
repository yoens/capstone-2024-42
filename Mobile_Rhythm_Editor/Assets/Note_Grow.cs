using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Grow : MonoBehaviour
{
    public float growSpeed = 1f; // 노트가 커지는 속도
    private bool isJudged = false; // 노트가 판정되었는지 여부

    void Start()
    {
        transform.localScale = Vector3.zero; // 노트의 초기 크기를 0으로 설정
    }

    void Update()
    {
        // 노트를 점점 커지게 함
        if (!isJudged)
        {
            transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        }

        // 터치 입력 감지
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                CheckTouch(touch.position);
            }
        }
    }

    void CheckTouch(Vector2 touchPosition)
    {
        // 터치 위치를 월드 좌표로 변환
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        worldPosition.z = 0; // Z 좌표는 무시

        // 터치 위치가 노트의 판정 범위 내에 있는지 확인
        if ((worldPosition - transform.position).magnitude < transform.localScale.x / 2)
        {
            JudgeNote();
        }
    }

    void JudgeNote()
    {
        // 노트 판정 로직
        isJudged = true;
        Debug.Log("노트 판정!");
        // 여기에 판정에 따른 점수 처리 등의 로직을 추가할 수 있습니다.

        Destroy(gameObject); // 노트 제거
    }
}