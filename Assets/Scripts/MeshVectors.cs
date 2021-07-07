using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СircleVertex
{
    private float _xPosition;
    private List<Vertical> _vertex = new List<Vertical>();
    private Vector3 _positionObject;
    private MeshDeformer _deformer;
    public float XPosition => _xPosition;
    public List<Vertical> Vertex => _vertex;

    public СircleVertex(float xPosition,Vector3 positionObject,MeshDeformer deformer)
    {
        _deformer = deformer;
        _xPosition = xPosition;
        _positionObject = positionObject;
    }

    public void AddVector(Vector3 vector3, int id,Vector3 scaleCube)
    {
        _vertex.Add(new Vertical(id,vector3,scaleCube,_deformer));
    }

    public void StartDeformation(float force)
    {
        for (int i = 0; i < _vertex.Count; i++)
        {
            _vertex[i].Deformation(force,true);
        }
    }
}
