using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentLine : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            // 판정 로직 실행
            Debug.Log("Note Hit!");

            Destroy(other.gameObject);

            // 추가적인 판정 기준을 적용하려면, 여기에 구현
        }
    }
}