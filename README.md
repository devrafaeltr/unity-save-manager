# Unity_SaveManager
Simple way to save and load local data from your game.


## **How to use:**
1. Download and import to your project
2. Done!

Fell free to change ```public class PlayerData``` to ```public struct PlayerData``` and/or change PlayerData's variable.  
___
### Notes  
To reference another class/struct in the PlayerData you should add `[System.Serializable]`:
```C#
[System.Serializable]
public class Inventory
{
  //Cool variables here
}

[System.Serializable]
public class PlayerData
{
  public Inventory playerInventory;
  //Coolest variables here
}
```
