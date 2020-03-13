using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class Manager_Save
{
    #region Save names
    public static readonly (string name, string format) playerData_SaveInfo = ("myPlayerData", ".dat");
    public static readonly (string name, string format) myBool_SaveInfo = ("myPlayerData", ".dat");
    public static readonly (string name, string format) myInt_SaveInfo = ("myPlayerData", ".dat");
    public static readonly (string name, string format) myString_SaveInfo = ("myPlayerData", ".dat");

    public static readonly (string name, string format) anotherCool_SaveInfo = ("someName", ".someFormat");
    #endregion    

    public static T LoadData<T>((string name, string format) saveData, System.Action resultCallback = null) where T : new()
    {
        T dataToLoad = new T();

        if (File.Exists(GetPath(saveData)))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileToOpen = File.Open(GetPath(saveData), FileMode.Open);
            dataToLoad = (T)binaryFormatter.Deserialize(fileToOpen);
            fileToOpen.Close();
#if UNITY_EDITOR
            Debug.Log("Loaded: " + PlayerDataToJson(dataToLoad));
#endif
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log($"No file found. Returning new {typeof(T)}");
        }
#endif

        resultCallback?.Invoke();

        return dataToLoad;
    }

    public static void SaveData<T>((string name, string format) saveData, T data, System.Action resultCallback = null)
    {

        if (data != null)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileToCreate = File.Open(GetPath(saveData), FileMode.OpenOrCreate);
            binaryFormatter.Serialize(fileToCreate, data);
            fileToCreate.Close();

#if UNITY_EDITOR
            Debug.Log("Saved: " + PlayerDataToJson(data));
#endif
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log($"data param is null. Nothing to save.");
        }
#endif

        resultCallback?.Invoke();
    }

    public static void DeleteData((string name, string format) saveData)
    {
        string tempPath = GetPath(saveData);
        if (File.Exists(tempPath))
        {
#if UNITY_EDITOR
            Debug.Log($"Deleted {saveData.name}{saveData.format} at {Application.persistentDataPath}");
#endif
            File.Delete(tempPath);
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log($"No file named {saveData.name}{saveData.format} found at {Application.persistentDataPath}");
        }
#endif
    }

    private static string GetPath((string name, string format) saveData)
    {
        return $"{Application.persistentDataPath}{saveData.name}{saveData.format}";
    }


    //Only for debug purposes
#if UNITY_EDITOR
    private static string PlayerDataToJson<T>(T data)
    {
        if (data == null)
        {
            return JsonUtility.ToJson(default(T));
        }
        else
        {
            return JsonUtility.ToJson(data);
        }
    }
#endif
}