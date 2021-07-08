using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleVertex
{
    private float _xPosition;
    private List<Vertical> _vertex = new List<Vertical>();
    private MeshDeformer _deformer;
    public float XPosition => _xPosition;
    public List<Vertical> Vertex => _vertex;

    public CircleVertex(float xPosition,MeshDeformer deformer)
    {
        _deformer = deformer;
        _xPosition = xPosition;
    }

    public void AddVector(Vector3 vector3, int id)
    {
        _vertex.Add(new Vertical(id,vector3,_deformer));
    }

    public void StartDeformation(float force)
    {
        for (int i = 0; i < _vertex.Count; i++)
        {
            _vertex[i].Deformation(force,true);
        }
    }
}
