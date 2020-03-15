using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public bool inUse = false;
    public int count = 0;

    public Item(bool random)
    {
        if(!random)
        {
            return;
        }

        inUse = Random.Range(0, 2) == 0;
        count = Random.Range(0, 6);
    }
}

[System.Serializable]
public class Inventory
{
    public List<Item> myItems = new List<Item>();

    public Inventory(int itemCount)
    {
        GetNewItems(itemCount);
    }

    public void GetNewItems(int itemCount)
    {
        for (int i = 0; i < itemCount; i++)
        {
            myItems.Add(new Item(true));
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public Inventory myInventory = null;
    public int myCoolInt = 0;
    public string myCoolString = "";
    public bool myCoolBool = false;

    public void Randomize()
    {
        myCoolInt = Random.Range(0, 9999);
        myCoolString = System.DateTime.Now.Second.ToString("x4");
        myCoolBool = Random.Range(0, 9999) % 2 == 0;

        int itemCount = Random.Range(0, 11);
        myInventory = new Inventory(itemCount);
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
        Manager_Save.SaveData(Manager_Save.playerData_SaveInfo, myCustomData, null);
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