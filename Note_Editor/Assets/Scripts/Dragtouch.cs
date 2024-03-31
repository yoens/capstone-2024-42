using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Dragtouch : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public GameObject objectToRotate; // 회전시킬 오브젝트 지정
    private Vector2 lastDragPosition;
    public float rotationSpeed = 0.5f; // 회전 속도 조절 변수

    public void OnPointerDown(PointerEventData eventData)
    {
        // 터치 시작 위치를 기록
        lastDragPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentDragPosition = eventData.position;
        Vector2 direction = currentDragPosition - lastDragPosition;

        if (direction.x > 0) // 오른쪽으로 드래그
        {
            objectToRotate.transform.Rotate(Vector3.forward, -rotationSpeed * direction.magnitude);
        }
        else if (direction.x < 0) // 왼쪽으로 드래그
        {
            objectToRotate.transform.Rotate(Vector3.forward, rotationSpeed * direction.magnitude);
        }

        lastDragPosition = currentDragPosition; // 드래그 위치 업데이트
    }
}