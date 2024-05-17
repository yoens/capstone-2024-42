using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public float time;
    public string type;
}

[System.Serializable]
public class NoteDataCollection
{
    public NoteData[] Notes;
}

public class NoteSpawner : MonoBehaviour
{
    public AudioSource audioSource;
    public ObjectPoolManager poolManager;
    public string jsonFilePath = "Assets/Ripple - Everyday  DnB  NCS - Copyright Free Music.json";
    private List<NoteData> notesToSpawn;
    public Transform[] spawnPoints;
    public Transform centerPoint;
    private Dictionary<string, int> typeToIndexMap = new Dictionary<string, int> {
        { "Note_R", 2 }, 
        { "Note_G", 1 }, 
        { "Note_Y", 0 }
    };

    void Start()
    {
        LoadNotesData();
        audioSource.Play();
    }

    void Update()
    {
        if (notesToSpawn.Count > 0 && audioSource.isPlaying)
        {
            while (notesToSpawn.Count > 0 && audioSource.time >= notesToSpawn[0].time)
            {
                SpawnNote(notesToSpawn[0]);
                notesToSpawn.RemoveAt(0);
            }
        }
    }

    void LoadNotesData()
    {
        string jsonText = File.ReadAllText(jsonFilePath);
        NoteDataCollection loadedData = JsonUtility.FromJson<NoteDataCollection>(jsonText);
        if (loadedData != null && loadedData.Notes != null)
        {
            notesToSpawn = new List<NoteData>(loadedData.Notes.OrderBy(note => note.time));
        }
        else
        {
            Debug.LogError("Failed to load notes data or notes are null.");
        }
    }

    void SpawnNote(NoteData noteData)
    {
        GameObject note = poolManager.GetObject(noteData.type);
        if (!typeToIndexMap.TryGetValue(noteData.type, out int spawnIndex) || spawnIndex >= spawnPoints.Length)
        {
            Debug.LogError("Invalid note type or spawn index out of range: " + noteData.type);
            return;
        }

        Transform spawnLocation = spawnPoints[spawnIndex];
        note.transform.position = spawnLocation.position;

        CircularMotion motion = note.GetComponent<CircularMotion>();
        if (motion == null)
        {
            motion = note.AddComponent<CircularMotion>();
        }
        motion.centerPoint = centerPoint;
        motion.Initialize(spawnLocation.position);
    }
}
