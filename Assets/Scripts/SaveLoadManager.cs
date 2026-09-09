using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class  SaveLoadManager : MonoBehaviour
{
    [SerializeField] private EntryListImporter importer;
    private UniPlatData hoistedData;

    private void Awake()
    {
        hoistedData = loadData();
    }

    [System.Serializable]
    private struct UniPlatData
    {
        public Dictionary<string, bool> Tasks;
        public Dictionary<string, bool> AppUsage;
    }

    public bool GetTaskStatus(string description)
    {
        bool isCompleted;
        if (hoistedData.Tasks.TryGetValue(description, out isCompleted))
        {
            return isCompleted;
        }
        else
        {
            Debug.LogWarning($"SaveLoadManager: Task '{description}' not found in save data.");
            return false; // Default to false if the task is not found
        }
    }

    public bool GetAppUsageStatus(string appName)
    {
        bool isUsed;
        if (hoistedData.AppUsage.TryGetValue(appName, out isUsed))
        {
            return isUsed;
        }
        else
        {
            Debug.LogWarning($"SaveLoadManager: App '{appName}' not found in save data.");
            return false; // Default to false if the app is not found
        }
    }

    private void OnApplicationQuit()
    {
        SaveHoistedData();
    }


    public void SaveHoistedData()
    {
        string json = JsonUtility.ToJson(hoistedData);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);
    }

    private UniPlatData loadData()
    {
        try
        {
            string data = File.ReadAllText(Application.persistentDataPath + "/saveData.json");
            
            if (data is string json)
            {
                
                return JsonUtility.FromJson<UniPlatData>(json);
            }
            else
            {
                Debug.LogWarning("SaveLoadManager: save data not json, creating new data.");
                return EmptyUniPlatData();
            }
        }
        catch (FileNotFoundException)
        {
            Debug.LogWarning("SaveLoadManager: save data not found, creating new data.");
            return EmptyUniPlatData();
        }
    }

    private UniPlatData EmptyUniPlatData()
    {
        UniPlatData data = new UniPlatData();
        data.Tasks = new Dictionary<string, bool>();

        string[] taskNames = importer.GetEntryDescriptions();
        foreach (string taskName in taskNames) {
            data.Tasks[taskName] = false; // Initialize all tasks as not completed
        }

        string[] appNames = importer.GetFilters();
        foreach (string appName in appNames) {
             data.AppUsage[appName] = false; // Initialize all apps as not used
        }

        return data;
    }

    private void OnDisable()
    {
        SaveHoistedData();
    }
}



[System.Serializable]
public struct LevelData
{
    public float bestTime;
    public bool isCompleted;
}
