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
        myCoolString = System.DateTime.Now.Second.ToString("x1");
        myCoolBool = Random.Range(0, 9999) % 2 == 0;
    }
}
public class Manager_SaveExample : MonoBehaviour
{

    #region Editor UI variables
    public Text stringTest = null;
    public Text intTest = null;
    public Text boolTest = null;
    public Text charTest = null;
    public Text playerDataJsonTest = null;
    #endregion

    private int myIntData = 0;
    private string myStringData;
    private char myChar = '1';
    private bool myBoolData = false;
    private PlayerData myCustomData = new PlayerData();

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        Manager_Save.LoadData<int>(Manager_Save.myInt_SaveInfo, null);
        Manager_Save.LoadData<string>(Manager_Save.myString_SaveInfo, null);
        Manager_Save.LoadData<char>(Manager_Save.myChar_SaveInfo, null);
        Manager_Save.LoadData<bool>(Manager_Save.myBool_SaveInfo, null);
        Manager_Save.LoadData<PlayerData>(Manager_Save.playerData_SaveInfo, null);

        ChangeUI();
    }

    public void Save()
    {
        Manager_Save.SaveData(Manager_Save.myInt_SaveInfo, myCustomData, null);
        Manager_Save.SaveData(Manager_Save.myString_SaveInfo, myCustomData, null);
        Manager_Save.SaveData(Manager_Save.myChar_SaveInfo, myCustomData, null);
        Manager_Save.SaveData(Manager_Save.myBool_SaveInfo, myCustomData, null);
        Manager_Save.SaveData(Manager_Save.playerData_SaveInfo, myCustomData, null);
    }

    public void Delete()
    {
        Manager_Save.DeleteData(Manager_Save.myInt_SaveInfo);
        Manager_Save.DeleteData(Manager_Save.myString_SaveInfo);
        Manager_Save.DeleteData(Manager_Save.myChar_SaveInfo);
        Manager_Save.DeleteData(Manager_Save.myBool_SaveInfo);
        Manager_Save.DeleteData(Manager_Save.playerData_SaveInfo);
    }

    public void GenerateRandom()
    {
        myIntData = Random.Range(0, 9999);
        myStringData = System.DateTime.Now.Millisecond.ToString("x1");
        myChar = (char)Random.Range(0, 50);
        myBoolData = Random.Range(0, 9999) % 2 == 0;
        myCustomData.Randomize();

        ChangeUI();
    }

    private void ChangeUI()
    {
        stringTest.text = myStringData;
        intTest.text = myIntData.ToString();
        boolTest.text = myBoolData.ToString();
        charTest.text = myChar.ToString();
        playerDataJsonTest.text = JsonUtility.ToJson(myCustomData).Replace(",", ", \n");
    }
}