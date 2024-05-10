using UnityEngine;
using TMPro;
using System.Collections;

public class RandomMarkers : MonoBehaviour
{
    public Transform centerPoint; // 회전의 중심점
    public ObjectPool markerPool; // 마커용 오브젝트 풀
    public GameObject explosionEffectPrefab; // 터지는 효과 프리팹
    public TextMeshProUGUI countdownText; // TextMesh Pro UI 텍스트
    public float markerLifetime = 3f; // 마커의 생존 시간
    public float angleBetweenMarkers = 60f; // 두 마커 사이의 각도
    public int scorePerSuccess = 100; // 성공 시 부여될 점수

    private GameObject markerA, markerB;
    private float baseAngle; // 기준 각도
    private float accumulatedRotation = 0f; // 누적 회전 각도
    private float lastAngle = 0f; // 마지막 확인된 각도
    private bool isRotating = false; // 회전이 시작되었는지 확인

    void Start()
    {
        StartCoroutine(ShowMarkersWithCountdown());
    }

    IEnumerator ShowMarkersWithCountdown()
    {
        while (true)
        {
            int countdown = 3; // 카운트다운 3초 시작
            while (countdown > 0)
            {
                countdownText.text = countdown.ToString();
                yield return new WaitForSeconds(1f);
                countdown--;
            }

            countdownText.text = ""; // 카운트다운 텍스트 클리어
            baseAngle = Random.Range(0f, 360f); // 랜덤 기준 각도 설정
            lastAngle = baseAngle; // 초기 회전 각도 설정
            accumulatedRotation = 0f; // 누적 회전 초기화
            isRotating = true; // 회전 시작 표시
            ActivateMarkers(); // 마커 활성화
            yield return new WaitForSeconds(markerLifetime); // 마커의 생존 시간동안 대기

            RemoveMarkers();
            isRotating = false; // 회전 종료
        }
    }

    void ActivateMarkers()
    {
        Vector3 positionA = GetPositionOnCircle(baseAngle);
        Vector3 positionB = GetPositionOnCircle(baseAngle + angleBetweenMarkers);

        markerA = markerPool.GetObject();
        markerA.transform.position = positionA;
        markerA.transform.rotation = Quaternion.LookRotation(Vector3.forward, centerPoint.position - positionA);

        markerB = markerPool.GetObject();
        markerB.transform.position = positionB;
        markerB.transform.rotation = Quaternion.LookRotation(Vector3.forward, centerPoint.position - positionB);
    }

    void RemoveMarkers()
    {
        if (markerA != null)
        {
            markerPool.ReturnObject(markerA);
            markerA = null;
        }
        if (markerB != null)
        {
            markerPool.ReturnObject(markerB);
            markerB = null;
        }
    }
    public void UpdateRotation(float currentAngle)
    {
        // 현재 각도에 따른 추가 로직 구현
        // 예: 현재 각도를 기반으로 마커의 상태를 업데이트하거나, 특정 조건을 체크
        Debug.Log("Current Angle Updated: " + currentAngle);
        // 여기서 각도를 사용하여 게임 로직을 업데이트할 수 있습니다.
    }

    Vector3 GetPositionOnCircle(float angle)
    {
        float x = centerPoint.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * 3f; // 반지름 3 가정
        float y = centerPoint.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * 3f;
        return new Vector3(x, y, 0);
    }
}
