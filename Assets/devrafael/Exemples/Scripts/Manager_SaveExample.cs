using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public uint saveVersion = 0;
    public int myCoolInt = 0;
    public string myCoolString = "";
    public bool myCoolBool = false;
}
public class Manager_SaveExample : MonoBehaviour
{
    private int myIntData = 0;
    private string myStringData = "";
    private bool myBoolData = false;
    private List<GameObject> myGameObjectsData = new List<GameObject>();
    private PlayerData myCustomData = null;

    public void Load()
    {
        myCustomData = Manager_Save.LoadData<PlayerData>(Manager_Save.myBool_SaveInfo);
        myCustomData.myCoolInt++;
        myCustomData.myCoolBool = !myCustomData.myCoolBool;
        myCustomData.myCoolString = System.DateTime.Now.Millisecond.ToString();
    }

    public void Save()
    {
        Manager_Save.SaveData(Manager_Save.myBool_SaveInfo, myCustomData, null);
    }

    public void Delete()
    {
        Manager_Save.DeleteData(Manager_Save.playerData_SaveInfo);
    }
}