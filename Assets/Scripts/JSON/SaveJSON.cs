using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveJSON : MonoBehaviour
{
    [SerializeField] SaveData data = new SaveData();

    string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/data.save";
        Debug.Log(path);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        
        Debug.Log("Saved Game "+ json);
    }

    public void LoadGame()
    {
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, data);

        Debug.Log("Loaded Game " + json);

    }
}
