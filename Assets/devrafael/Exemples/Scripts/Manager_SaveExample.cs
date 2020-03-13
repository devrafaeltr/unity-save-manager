using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public int myCoolInt = 0;
    public string myCoolString = "";
    public bool myCoolBool = false;

    public void Randomize()
    {
        myCoolInt = Random.Range(0, 9999);
        myCoolString = System.DateTime.Now.Second.ToString("x4");
        myCoolBool = Random.Range(0, 9999) % 2 == 0;
    }
}
public class Manager_SaveExample : MonoBehaviour
{

    #region Editor UI variables
    public Text playerDataJsonTest = null;
    #endregion

    private PlayerData myCustomData = new PlayerData();

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        myCustomData = Manager_Save.LoadData<PlayerData>(Manager_Save.playerData_SaveInfo, null);

        ChangeUI();
    }

    public void Save()
    {
        Manager_Save.SaveData<PlayerData>(Manager_Save.playerData_SaveInfo, myCustomData, null);
    }

    public void Delete()
    {
        Manager_Save.DeleteData(Manager_Save.playerData_SaveInfo);
    }

    public void GenerateRandom()
    {
        myCustomData.Randomize();

        ChangeUI();
    }

    private void ChangeUI()
    {
        playerDataJsonTest.text = JsonUtility.ToJson(myCustomData).Replace(",", ", \n");
    }
}