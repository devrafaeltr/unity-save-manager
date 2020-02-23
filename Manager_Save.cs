using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public class PlayerData
{
    //You can change uint type if you think that the player will call Save() more than 2,147,483,647 times
    public uint saveVersion;
    
    //Another cool variables here
}

public class Manager_Save
{
    private static string savePath = "";
    private static bool isInitialized = false;

    public static PlayerData GetPlayerData { get; private set; } = new PlayerData();

    public static void Init()
    {
        if (isInitialized)
        {
            return;
        }

        //You can change to any name and format like: "/banana.fruit"
        savePath = Application.persistentDataPath + "/save.dat";
        
        isInitialized = true;
    }

    public static void LoadGame(System.Action resultCallback = null)
    {
        Init();
        LocalLoad();
    }

    public static void SaveGame(System.Action resultCallback = null)
    {
        //Uncomment Init() if will call SaveGame() before LoadGame() in the game timeline
        //Init();
        GetPlayerData.saveVersion++;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileToCreate = File.Create(savePath);
        binaryFormatter.Serialize(fileToCreate, GetPlayerData);
        fileToCreate.Close();

        Debug.Log("Saved: " + PlayerDataToJson(GetPlayerData));
    }

    public static void DeleteSave(uint newVersion)
    {
        //This resets the saveVersion
        GetPlayerData = new PlayerData();
        SaveGame();
    }

    private static void LocalLoad()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileToOpen = File.Open(savePath, FileMode.Open);
            GetPlayerData = (PlayerData)binaryFormatter.Deserialize(fileToOpen);
            fileToOpen.Close();
        }

        Debug.Log("Loaded: " + PlayerDataToJson(GetPlayerData));
    }

    //Only for debug purposes
    private static string PlayerDataToJson(PlayerData playerData)
    {
        if (playerData == null)
        {
            return JsonUtility.ToJson(new PlayerData());
        }
        else
        {
            return JsonUtility.ToJson(playerData);
        }
    }
}
