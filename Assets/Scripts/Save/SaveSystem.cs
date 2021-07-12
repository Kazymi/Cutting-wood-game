using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
   private SaveData _saveData;
   private readonly SaveManager _saveManager = new SaveManager();

   public void SaveNewCircle(List<float> circleVertices)
   {
     _saveData.AddNewCircles(circleVertices);
     _saveManager.Save(_saveData);
   }

   public void DeleteAllCircle()
   {
       _saveData = new SaveData();
       _saveManager.Save(_saveData);
   }
   public List<float> GetRandomSavedCircle()
   {
       return _saveData.GetRandomSavedCircle();
   }
   
   private void Awake()
   {
       _saveData = _saveManager.Load();
   }
}
