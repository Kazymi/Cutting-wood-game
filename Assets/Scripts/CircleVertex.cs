using System.Collections.Generic;
using UnityEngine;

public class CircleVertex
{
    private float _xPosition;
    private List<Vertex> _vertex = new List<Vertex>();
    private MeshDeformer _deformer;
    private float magnitude;

    private Vector3 _startPos;
    private Vector3 _motionVector3;
    public float XPosition => _xPosition;
    public List<Vertex> Vertex => _vertex;

    public float Magnitude
    {
        get => magnitude;
    }

    public CircleVertex(float xPosition, MeshDeformer deformer)
    {
        _deformer = deformer;
        _xPosition = xPosition;
    }

    public void AddVector(Vector3 vector3, int id)
    {
        _vertex.Add(new Vertex(id, vector3));
        if (_startPos == Vector3.zero)
        {
            _startPos = vector3;
            var vecrot3 = (vector3 - new Vector3(vector3.x, 0, 0));
            _motionVector3 = vecrot3.normalized;
            magnitude = vecrot3.magnitude;
            Debug.DrawLine(vector3, new Vector3(vector3.x, 0, 0), Color.blue, 1000);
        }
    }

    public void StartDeformation(float force, bool split)
    {
        if (magnitude <= 0) return;
        var startMagnitude = _motionVector3 * force * Time.deltaTime;
        magnitude -= startMagnitude.magnitude;
        if (magnitude <= 0)
        {
            if (split) _deformer.SplitMesh(_xPosition);
            magnitude = 0;
            for (int i = 0; i < _vertex.Count; i++)
            {
                _vertex[i].Deformation(0);
            }
        }
        else
            for (int i = 0; i < _vertex.Count; i++)
            {
                _vertex[i].Deformation(startMagnitude.magnitude);
            }
    }

    public void Initialize(float magnitude)
    {
        if (this.magnitude != magnitude)
            for (int i = 0; i < _vertex.Count; i++)
            {
                _vertex[i].Deformation(this.magnitude - magnitude);
            }

        this.magnitude = magnitude;
    }
}