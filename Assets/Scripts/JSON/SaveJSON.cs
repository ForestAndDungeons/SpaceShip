using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveJSON
{
    public SaveData _data;
    string _path;

    public SaveJSON()
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