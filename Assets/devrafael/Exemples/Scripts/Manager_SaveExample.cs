using System.Collections.Generic;
using UnityEngine;

public class Manager_SaveExample : MonoBehaviour
{
    private int myIntData = 0;
    private string myStringData = "";
    private bool myBoolData = false;
    private List<GameObject> myGameObjectsData = new List<GameObject>();
    private PlayerData myCustomData = null;

    public void Load()
    {
        myCustomData = Manager_Save.LoadData<PlayerData>(Manager_Save.playerData_SaveInfo);
        myCustomData.myCoolInt++;
        myCustomData.myCoolBool = !myCustomData.myCoolBool;
        myCustomData.myCoolString = System.DateTime.Now.Millisecond.ToString();
    }

    public void Save()
    {
        Manager_Save.SaveData(Manager_Save.playerData_SaveInfo, myCustomData, null);
    }

    public void Delete()
    {
        Manager_Save.DeleteData(Manager_Save.playerData_SaveInfo);
    }
}