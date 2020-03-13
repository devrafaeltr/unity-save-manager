using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class Manager_Save
{
    #region Save names
    //Template
    //public static readonly (string name, string format) anotherCool_SaveInfo = ("someName", ".someFormat");

    public static readonly (string name, string format) myInt_SaveInfo = ("myPlayerData", ".dat");
    public static readonly (string name, string format) myString_SaveInfo = ("myPlayerData", ".dat");
    public static readonly (string name, string format) myChar_SaveInfo = ("myPlayerData", ".dat");
    public static readonly (string name, string format) myBool_SaveInfo = ("myPlayerData", ".dat");
    public static readonly (string name, string format) playerData_SaveInfo = ("myPlayerData", ".dat");
    #endregion    

    public static T LoadData<T>((string name, string format) saveData, System.Action<bool> resultCallback = null)
    {
        T dataToLoad = default(T);
        bool success = false;

        if (File.Exists(GetPath(saveData)))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileToOpen = File.Open(GetPath(saveData), FileMode.Open);

            string encryptedJsonData = (string)binaryFormatter.Deserialize(fileToOpen);
            string decyptedJsonData = SimpleEncryption.Decrypt(encryptedJsonData);

            dataToLoad = FromJson<T>(decyptedJsonData);
            fileToOpen.Close();

            success = true;

#if UNITY_EDITOR
            Debug.Log($"Loaded JSON encrypted as: {encryptedJsonData}\nDecrypted to {typeof(T)}: {decyptedJsonData}");
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

    public static void SaveData<T>((string name, string format) saveData, T data, System.Action<bool> resultCallback = null)
    {
        bool success = false;

        if (data != null)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileToCreate = File.Open(GetPath(saveData), FileMode.OpenOrCreate);
            //binaryFormatter.Serialize(fileToCreate, data);

            string decyptedJsonData = DataToJson(data);
            string encryptedJsonData = SimpleEncryption.Encrypt(decyptedJsonData);


            binaryFormatter.Serialize(fileToCreate, encryptedJsonData);
            fileToCreate.Close();

            success = true;

#if UNITY_EDITOR
            Debug.Log($"Saved {typeof(T)} as JSON: {decyptedJsonData}\nEncrypted to: {encryptedJsonData}");
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

    public static void DeleteData((string name, string format) saveData, System.Action<bool> resultCallback = null)
    {
        bool success = false;
        string tempPath = GetPath(saveData);
        if (File.Exists(tempPath))
        {
#if UNITY_EDITOR
            Debug.Log($"Deleted {saveData.name}{saveData.format} at {Application.persistentDataPath}");
#endif
            File.Delete(tempPath);
            success = true;
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log($"No file named {saveData.name}{saveData.format} found at {Application.persistentDataPath}");
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
            return JsonUtility.ToJson(data);
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError($"data param is nul. Nothing to convert. Returning an empty string.");
            return "";
        }
#endif
    }

    private static T FromJson<T>(string jsonData)
    {
        if (!string.IsNullOrEmpty(jsonData))
        {
            return JsonUtility.FromJson<T>(jsonData);
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError($"jsonData param is empty. Nothing to convert. Returning a default value for {typeof(T)}.");
            return default;
        }
#endif
    }
}