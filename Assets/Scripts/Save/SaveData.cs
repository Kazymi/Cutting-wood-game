using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SaveData
{
   [SerializeField] private List<SavedCircle> _savedCircles = new List<SavedCircle>();

   public void AddNewCircles(List<float> circleVertices)
   {
      _savedCircles.Add(new SavedCircle(circleVertices));
   }

   public List<float> GetRandomSavedCircle()
   {
      if (_savedCircles.Count == 0)
      {
         Debug.Log("Not circles");
         return null;
      }
      else
      {
         return _savedCircles[Random.Range(0, _savedCircles.Count)].CircleVertices;
      }
   }
}
