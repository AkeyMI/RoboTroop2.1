using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveJson : MonoBehaviour
{
    public void ConvertToJson(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        SaveJsonToFile(json);
    }

    private void SaveJsonToFile(string json)
    {
        string saveDirectoryName = "saves";
        string saveDirectoryPath = Path.Combine(Application.persistentDataPath, saveDirectoryName);

        string fileName = "save.json";
        string filePath = Path.Combine(saveDirectoryPath, fileName);

        if (!Directory.Exists(saveDirectoryPath))
        {
            Directory.CreateDirectory(saveDirectoryPath);
        }

        Debug.Log("JSON saved: " + filePath);
        File.WriteAllText(filePath, json);
    }

    public SaveData Read_Jason()
    {
        string json = ReadJsonFile();
        SaveData jsonData = JsonUtility.FromJson<SaveData>(json);
        return jsonData;
    }

    private string ReadJsonFile()
    {
        string saveDirectoryName = "saves";
        string saveDirectoryPath = Path.Combine(Application.persistentDataPath, saveDirectoryName);

        string fileName = "save.json";
        string filePath = Path.Combine(saveDirectoryPath, fileName);

        if (!Directory.Exists(saveDirectoryPath))
        {
            ConvertToJson(CreateFirtsSaveData());
        }

        string text = File.ReadAllText(filePath);
        return text;
    }

    private SaveData CreateFirtsSaveData()
    {
        SaveData data = new SaveData();

        data.canyonGuy = false;
        data.havyMachineGuy = false;
        data.machineGuy = false;
        data.semigunGuy = false;
        data.shotGuy = false;
        data.triplegunGuy = false;

        return data;
    }
}
