using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject notePrefab;
    public Camera camera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            PlaceNote();
        }
    }

    void PlaceNote()
    {
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(notePrefab, mousePos, Quaternion.identity);
    }
}