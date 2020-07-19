using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class Manager_Save
{
    #region SaveInfos
    //Template
    //public static readonly (string name, string format) anotherCool_SaveInfo = ("someName", ".someFormat");
    public static readonly (string name, string format) playerData_SaveInfo = ("myPlayerData", ".dat");
    #endregion    

    public static T LoadData<T>((string name, string format) saveInfo, System.Action<bool> resultCallback = null) where T : new()
    {
        T dataToLoad = new T();
        bool success = false;
        string path = GetPath(saveInfo);
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileToLoad = File.Open(path, FileMode.Open);

            dataToLoad = (T)binaryFormatter.Deserialize(fileToLoad);

            fileToLoad.Close();

            success = true;

#if UNITY_EDITOR
            Debug.Log($"Loaded {typeof(T)} from {path}.");
#endif
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log($"No file found. Returning new {typeof(T)}");
        }
#endif

        resultCallback?.Invoke(success);

        return dataToLoad;
    }

    public static void SaveData<T>((string name, string format) saveInfo, T data, System.Action<bool> resultCallback = null)
    {
        bool success = false;
        string path = GetPath(saveInfo);

        if (data != null)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream savedFile = File.Open(path, FileMode.OpenOrCreate);

            binaryFormatter.Serialize(savedFile, data);
            savedFile.Close();

            success = true;

#if UNITY_EDITOR
            Debug.Log($"Saved {typeof(T)} at {path}.");
#endif
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError($"data param is null. Nothing to save.");
        }
#endif

        resultCallback?.Invoke(success);
    }

    public static void DeleteData((string name, string format) saveInfo, System.Action<bool> resultCallback = null)
    {
        bool success = false;
        string tempPath = GetPath(saveInfo);
        if (File.Exists(tempPath))
        {
#if UNITY_EDITOR
            Debug.Log($"Deleted {saveInfo.name}{saveInfo.format} at {Application.persistentDataPath}");
#endif
            File.Delete(tempPath);
            success = true;
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log($"No file named {saveInfo.name}{saveInfo.format} found at {Application.persistentDataPath}");
        }
#endif

        resultCallback?.Invoke(success);
    }

    private static string GetPath((string name, string format) saveData)
    {
        return $"{Application.persistentDataPath}/{saveData.name}{saveData.format}";
    }
}