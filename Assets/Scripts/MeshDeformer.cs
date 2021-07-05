using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    private Mesh _deformingMesh;
    private Vector3[] _originalVertices;
    private Vector3[] _displacedVertices;
    private Vector3[] _vertexVelocities;

    private void Start()
    {
        _deformingMesh = GetComponent<MeshFilter>().mesh;
        _originalVertices = _deformingMesh.vertices;
        _displacedVertices = new Vector3[_originalVertices.Length];
        for (int i = 0; i < _originalVertices.Length; i++)
        {
            _displacedVertices[i] = _originalVertices[i];
        }

        _vertexVelocities = new Vector3[_originalVertices.Length];
    }

    private void Update()
    {
        _deformingMesh.vertices = _displacedVertices;
        _deformingMesh.RecalculateNormals();
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        Debug.Log(point);
        var xs = new float[_displacedVertices.Length];
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            xs[i] = _displacedVertices[i].x;
        }

        var closest = xs.OrderBy(v => Math.Abs((long) v - point.x)).First();
        Debug.Log(closest);
        SetDeforming(closest, force);
    }

    private void SetDeforming(float pointX, float force)
    {
        force = force * Time.deltaTime;
        var idCurrentVectors = new List<int>();
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            if (_displacedVertices[i].x == pointX)
            {
                idCurrentVectors.Add(i);
            }
        }

        var minY = 10000f;
        var maxY = 0f;
        
        foreach (var i in idCurrentVectors)
        {
            if (minY > _displacedVertices[i].y) minY = _displacedVertices[i].y;
            if (maxY < _displacedVertices[i].y) maxY = _displacedVertices[i].y;
        }

        var meanValue = maxY / 2;
        foreach (var i in idCurrentVectors)
        {
            if (_displacedVertices[i].y == maxY || _displacedVertices[i].y > meanValue) _displacedVertices[i].y -= force;
            if (_displacedVertices[i].y == minY || _displacedVertices[i].y < meanValue) _displacedVertices[i].y += force;
        }
    }

    private void UpdateVertex(int i)
    {
        Vector3 velocity = _vertexVelocities[i];
        _vertexVelocities[i] = Vector3.zero;
        _displacedVertices[i] += velocity * Time.deltaTime;
    }
}