using UnityEngine;
public class ObjectPoolManager : MonoBehaviour
{
    public ObjectPool poolNoteG;
    public ObjectPool poolNoteY;
    public ObjectPool poolNoteR;

    public GameObject GetNote(string type)
    {
        switch (type)
        {
            case "Note_G":
                return poolNoteG.GetObject();
            case "Note_Y":
                return poolNoteY.GetObject();
            case "Note_R":
                return poolNoteR.GetObject();
            default:
                Debug.LogError("Invalid note type requested.");
                return null;
        }
    }

    public void ReturnNote(GameObject note, string type)
    {
        switch (type)
        {
            case "Note_G":
                poolNoteG.ReturnObject(note);
                break;
            case "Note_Y":
                poolNoteY.ReturnObject(note);
                break;
            case "Note_R":
                poolNoteR.ReturnObject(note);
                break;
            default:
                Debug.LogError("Invalid note type returned.");
                break;
        }
    }
}
