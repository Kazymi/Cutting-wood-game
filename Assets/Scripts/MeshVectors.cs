using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVectors
{
    public float XPosition;
    public List<ClassVector> Vectors = new List<ClassVector>();

    public float minY = 9999f;
    public float maxY = 0f;
    public MeshVectors(float xPosition)
    {
        XPosition = xPosition;
    }

    public void AddVector(Vector3 vector3, int id)
    {
        Vectors.Add(new ClassVector(id,vector3));
    }

    public void UpdateState()
    {
        foreach (var i in Vectors)
        {
            if (minY > i.Vector3.y) minY = i.Vector3.y;
            if (maxY < i.Vector3.y) maxY = i.Vector3.y;
        }
    }
}
