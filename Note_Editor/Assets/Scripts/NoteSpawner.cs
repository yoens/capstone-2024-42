using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public NoteManager noteManager; // NoteManager에 대한 참조
    public GameObject yellowNotePrefab, greenNotePrefab, dragNotePrefab; // 노트 프리팹
    public Transform spawnPoint; // 고정된 스폰 지점

    private Vector3 dragDirection = Vector3.right; // 초기 드래그 방향을 오른쪽으로 설정
    private bool isDragging = false; // 드래그 중인지 여부를 확인하는 플래그
    private float spawnRate = 0.1f; // 드래그 노트 생성 간격
    private float nextSpawnTime = 0f; // 다음 노트 생성 시간
    public AudioManager audioManager;

    void Update()
    {
        // 단일 노트 생성 (키보드 입력에 응답)
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SpawnNote(yellowNotePrefab, "Note_Y", spawnPoint.position);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnNote(greenNotePrefab, "Note_G", spawnPoint.position);
        }

        // 드래그 중인 경우 일정 간격으로 드래그 노트 생성
        if (isDragging && Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnRate;
            SpawnDragNote();
        }
    }

    public void StartDragging()
    {
        isDragging = true;
        // 드래그 시작 시 바로 드래그 노트 생성을 위해 nextSpawnTime을 초기화합니다.
        nextSpawnTime = Time.time;
    }

    public void UpdateDragDirection(Vector3 direction)
    {
        dragDirection = direction.normalized; // 드래그 방향 업데이트
    }

    public void StopDragging()
    {
        isDragging = false;
    }

    private void SpawnNote(GameObject prefab, string tag, Vector3 position)
    {
        GameObject note = Instantiate(prefab, position, Quaternion.identity);
        note.tag = tag;
        float musicTime = audioManager.GetMusicTime(); // 음악의 현재 재생 시간 가져오기
        noteManager.AddNote(musicTime, tag, position, Vector3.zero);
    }

    private void SpawnDragNote()
    {
        GameObject note = Instantiate(dragNotePrefab, spawnPoint.position, Quaternion.identity);
        DragNoteMovement movementComponent = note.GetComponent<DragNoteMovement>();
        if (movementComponent != null)
        {
            movementComponent.Initialize(dragDirection); // 노트 이동 방향 설정
        }
        float musicTime = audioManager.GetMusicTime(); // 음악의 현재 재생 시간 가져오기
        noteManager.AddNote(musicTime, "Drag_Note", spawnPoint.position, dragDirection);    
    }
}
