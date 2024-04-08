using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SFB;
public class NoteManager : MonoBehaviour
{
    public List<NoteData> notes = new List<NoteData>();

    public void SaveNotes()
    {
        var path = StandaloneFileBrowser.SaveFilePanel("Save Notes", "", "notes", "json");
        if (!string.IsNullOrEmpty(path))
        {
            string json = JsonUtility.ToJson(new NoteDataWrapper { Notes = notes }, true);
            File.WriteAllText(path, json);
        }
    }
    public void DeleteNote(float time)
    {
        notes.RemoveAll(note => note.time == time);
    }

    // 방향 정보를 포함하여 노트를 추가하는 메서드
    public void AddNote(float time, string type, Vector3 position, Vector3 direction)
    {
        notes.Add(new NoteData(time, type, position, direction));
    }

    [System.Serializable]
    public class NoteDataWrapper
    {
        public List<NoteData> Notes;
    }
}
