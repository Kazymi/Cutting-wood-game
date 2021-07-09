using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedCircle
{
  [SerializeField] private List<float> _circleVertices;

  public List<float> CircleVertices => _circleVertices;

  public SavedCircle(List<float> circleVertices)
  {
    _circleVertices = circleVertices;
  }
}
