using UnityEngine;

public class SaveManager
{
     private const string _nameSave = "SaveCuttingWood";

     public void Save(SaveData saveData)
     {
        var saveString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(_nameSave,saveString);
        PlayerPrefs.Save();
     }

     public SaveData Load()
     {
        var loadedString = PlayerPrefs.GetString(_nameSave);
        return JsonUtility.FromJson<SaveData>(loadedString) ?? new SaveData();
     }

}
