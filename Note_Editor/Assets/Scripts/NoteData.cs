using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public float time; // 노트가 생성된 시간
    public string type; // 노트의 타입
    public Vector3 position; // 노트의 위치
    public Vector3 direction; // 노트의 이동 방향

    // 생성자: 이동 방향을 포함
    public NoteData(float time, string type, Vector3 position, Vector3 direction)
    {
        this.time = time;
        this.type = type;
        this.position = position;
        this.direction = direction; // 이동 방향 설정
    }
}
