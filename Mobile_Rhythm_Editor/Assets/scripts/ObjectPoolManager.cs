using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public ObjectPool poolNoteG;
    public ObjectPool poolNoteY;
    public ObjectPool poolNoteR;
    public ObjectPool poolMarker;  // 마커를 위한 오브젝트 풀 추가

    public GameObject GetObject(string type)
    {
        switch (type)
        {
            case "Note_G":
                return poolNoteG.GetObject();
            case "Note_Y":
                return poolNoteY.GetObject();
            case "Note_R":
                return poolNoteR.GetObject();
            case "Marker":
                return poolMarker.GetObject();  // 마커용 풀을 사용
            default:
                Debug.LogError("Invalid type requested: " + type);
                return null;
        }
    }

    public void ReturnObject(GameObject obj, string type)
    {
        switch (type)
        {
            case "Note_G":
                poolNoteG.ReturnObject(obj);
                break;
            case "Note_Y":
                poolNoteY.ReturnObject(obj);
                break;
            case "Note_R":
                poolNoteR.ReturnObject(obj);
                break;
            case "Marker":
                poolMarker.ReturnObject(obj);  // 마커 반환
                break;
            default:
                Debug.LogError("Invalid type returned: " + type);
                break;
        }
    }
}
