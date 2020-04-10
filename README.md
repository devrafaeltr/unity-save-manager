[![GitHub](https://img.shields.io/github/license/devrafael-source/Unity_SaveManager)](https://github.com/devrafael-source/Unity_SaveManager/blob/master/LICENSE)
# unity-save-manager
Simple way to save and load local data from your game.

___
## Getting started
### How to install:
1. Download and import to your project.
2. Done!

### How to use:
1. Go to the Manager_Save under `#region` SaveInfos.
2. Create a `public static (string name, string format) saveInfo = ("savedFileName", "savedFileFormat");` 
There is a template. You can use any name and any format.
3. Now, anywhere, you can call ```Manager_Save.SaveData(saveInfo, yourData)``` and ```Manager_Save.LoadData<type>(saveInfo, yourData)```.

___
### Notes  
Always use `[System.Serializable]` in the class or struct that you are using:
```C#
[System.Serializable]
public class SomeAnotherClass
{
  //Cool variables here
}

[System.Serializable]
public class SomeClass
{
  public SomeAnotherClass someAnotherClass;
  //Coolest variables here
}
```
When calling ```Manager_Save.LoadData<type>()```, does not forget to pass your variable type in the <>:
```C#  
  //Somewhere trying to load data:
  Manager_Save.LoadData<SomeClass>(saveInfo, playerDataVariabel)
  ```
  
You need to create a `public static (string name, string format) saveInfo` in the `Manager_Save` for **EACH** different save file. For example, if you want to save PlayerData and GameConfigs separately.
___
## Known limitations
Since Manager_Save uses JsonUtility it does not work for primitive type, yet. See more [here](https://docs.unity3d.com/ScriptReference/JsonUtility.ToJson.html).  

### Workarounds

If you want to save a primitive type, you can just wrap it in some class or struct.

You can change this:

```C#
public class SomeClass : MonoBehaviour
{
  int mySavedInt = 0;
  
  void SomeFunction()
  {
    Manager_Save.SaveData(saveInfo, mySavedInt);
    Manager_Save.LoadData<int>(saveInfo, mySavedInt);
  }
}
```
To this:
```C#
[System.Serializable]
public class SomeAnotherClass
{
  int mySavedInt = 0;
}

public class SomeClass : MonoBehaviour
{
  PlayerData mySavedData;
  
  void SomeFunction()
  {
    Manager_Save.SaveData(saveInfo, mySavedData);
    Manager_Save.LoadData<SomeAnotherClass>(saveInfo, mySavedData);
  }
}
```
