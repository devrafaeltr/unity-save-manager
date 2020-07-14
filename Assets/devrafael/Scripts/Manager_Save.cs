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

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileToOpen = File.Open(path, FileMode.Open);

            string encryptedJsonData = (string)binaryFormatter.Deserialize(fileToOpen);
            string decyptedJsonData = SimpleEncryption.Decrypt(encryptedJsonData);

            dataToLoad = FromJson<T>(decyptedJsonData);
            fileToOpen.Close();

            success = true;

            #if UNITY_EDITOR
            Debug.Log($"Loaded JSON at {path} \nEncrypted as: {encryptedJsonData}\nDecrypted to {typeof(T)}: {decyptedJsonData}");
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
            FileStream fileToCreate = File.Open(path, FileMode.OpenOrCreate);

            string decyptedJsonData = DataToJson((T)data);
            string encryptedJsonData = SimpleEncryption.Encrypt(decyptedJsonData);


            binaryFormatter.Serialize(fileToCreate, encryptedJsonData);
            fileToCreate.Close();

            success = true;

        #if UNITY_EDITOR
            Debug.Log($"Saved {typeof(T)} at {path}\nas JSON: {decyptedJsonData}\nEncrypted to: {encryptedJsonData}");
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
        return $"{Application.persistentDataPath}{saveData.name}{saveData.format}";
    }

    private static string DataToJson(object data)
    {
        if (data != null)
        {
            #if UNITY_EDITOR
            return JsonUtility.ToJson(data, true);
            #else
            return JsonUtility.ToJson(data);
            #endif

        }
        else
        {
            #if UNITY_EDITOR
            Debug.LogError($"data param is nul. Nothing to convert. Returning an empty string.");
            #endif
            return "";
        }
    }

    private static T FromJson<T>(string jsonData)
    {
        if (!string.IsNullOrEmpty(jsonData))
        {
            return JsonUtility.FromJson<T>(jsonData);
        }
        else
        {
            #if UNITY_EDITOR
            Debug.LogError($"jsonData param is empty. Nothing to convert. Returning a default value for {typeof(T)}.");
            #endif
            return default;
        }
    }
}
