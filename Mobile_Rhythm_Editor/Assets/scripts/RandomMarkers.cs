using UnityEngine;
using TMPro;
using System.Collections;

public class RandomMarkers : MonoBehaviour
{
    public Transform centerPoint; // 회전의 중심점
    public GameObject explosionEffectPrefab; // 터지는 효과 프리팹
    public TextMeshProUGUI countdownText; // TextMesh Pro UI 텍스트
    public float markerLifetime = 3f; // 마커의 생존 시간
    public int scorePerSuccess = 100; // 성공 시 부여될 점수
    public ObjectPoolManager objectPoolManager; // 오브젝트 풀 매니저 참조

    private GameObject markerA, markerB;
    private float lastAngle = 0f; // 마지막 확인된 각도
    private bool isRotating = false; // 회전이 시작되었는지 확인
    private float targetAngle; // 두 번째 마커의 각도

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

            countdownText.text = "";
            isRotating = true; // 회전 시작 표시
            ActivateMarkers(); // 마커 활성화
            yield return new WaitForSeconds(markerLifetime); // 마커의 생존 시간동안 대기

            RemoveMarkers();
            isRotating = false; // 회전 종료
        }
    }

    void ActivateMarkers()
    {
        markerA = objectPoolManager.GetObject("Marker");
        markerA.transform.position = GetPositionOnCircle(0); // 첫 번째 마커 위치
        OrientMarkerTowardsCenter(markerA);

        float randomAngle = Random.Range(-180f, 180f);
        markerB = objectPoolManager.GetObject("Marker");
        markerB.transform.position = GetPositionOnCircle(randomAngle); // 두 번째 마커 위치
        OrientMarkerTowardsCenter(markerB);
        targetAngle = randomAngle; // 두 번째 마커의 타겟 각도 저장
    }

    public void UpdateRotation(float currentAngle)
    {
        if (!isRotating) return;

        // 올바른 방향으로 충분히 회전했는지 확인
        if ((targetAngle > 0 && currentAngle >= targetAngle) || (targetAngle < 0 && currentAngle <= targetAngle))
        {
            CorrectRotation();
        }
    }

    void CorrectRotation()
    {
        // 점수 추가
        ScoreManager.Instance.AddScore(scorePerSuccess);
        Debug.Log("Correct rotation! Score: " + scorePerSuccess);

        // 폭발 효과 생성
        if (explosionEffectPrefab != null)
        {
            GameObject explosion = Instantiate(explosionEffectPrefab, centerPoint.position, Quaternion.identity);
            Destroy(explosion, 1f); // 1초 후 폭발 효과 제거
        }
        isRotating = false; // 회전 종료, 누적 각도 초기화
    }

    void RemoveMarkers()
    {
        if (markerA != null)
        {
            objectPoolManager.ReturnObject(markerA, "Marker");
            markerA = null;
        }
        if (markerB != null)
        {
            objectPoolManager.ReturnObject(markerB, "Marker");
            markerB = null;
        }
    }

    Vector3 GetPositionOnCircle(float angle)
    {
        float radius = 3f; // 반지름을 3으로 설정
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(centerPoint.position.x + Mathf.Cos(radian) * radius,
                           centerPoint.position.y + Mathf.Sin(radian) * radius,
                           0);
    }

    void OrientMarkerTowardsCenter(GameObject marker)
    {
        marker.transform.rotation = Quaternion.LookRotation(Vector3.forward, centerPoint.position - marker.transform.position);
    }
}
