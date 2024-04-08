using UnityEngine;
using UnityEngine.EventSystems;

public class DragTouch : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public NoteSpawner noteSpawner;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 NoteSpawner에 드래그 시작을 알림 (인자 없음)
        noteSpawner.StartDragging();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentDragPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        currentDragPosition.z = 0; // Z 축 값을 0으로 설정하여 2D 평면상에서의 위치로 변환
        Vector3 dragDirection = currentDragPosition - noteSpawner.spawnPoint.position; // 드래그 방향 계산

        noteSpawner.UpdateDragDirection(dragDirection); // NoteSpawner에 드래그 방향 업데이트
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        noteSpawner.StopDragging(); // 드래그 종료를 NoteSpawner에 알림
    }
}
