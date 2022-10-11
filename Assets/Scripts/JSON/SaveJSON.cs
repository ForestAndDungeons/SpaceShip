using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveJSON : MonoBehaviour
{
    public SaveData _data;
    string _path;

    void Awake()
    {
        _data = new SaveData();
        _path = Application.persistentDataPath + "/data.save";
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(_data);
        File.WriteAllText(_path, json);
    }

    public void LoadGame()
    {
        string json = File.ReadAllText(_path);
        JsonUtility.FromJsonOverwrite(json, _data);
    }
}