[![GitHub](https://img.shields.io/github/license/devrafael-source/unity-save-manager)](https://github.com/devrafael-source/unity-save-manager/blob/master/LICENSE)
# unity-save-manager
Simple way to save and load local data from your game.

___
## Getting started
### How to install:
1. Download from [Releases](https://github.com/devrafael-source/unity-save-manager/files/4944506/devrafael_SaveManager.zip).
2. Open your project, go to Assets > Import Package > Custom Package and choose the package OR double-click the package to import to the open project.

### How to use:
1. Go to the Manager_Save under `#region SaveInfos`.
2. Create a `public static (string name, string format) saveInfo = ("savedFileName", "savedFileFormat");` 
There is a template. You can use any name and any format.
3. Now, anywhere, you can call ```Manager_Save.SaveData(saveInfo, yourData)``` and ```Manager_Save.LoadData<type>(saveInfo, yourData)```.

### Settings:
You can change `SHOW_DEBUG` to `true` or `false` in the Manager_Save to choose wether or not to show debug logs.
___
### Notes  
Always use `[System.Serializable]` in the class or struct that you are saving:
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
Limited to [Unity's Serialization rules](https://docs.unity3d.com/Manual/script-Serialization.html).
