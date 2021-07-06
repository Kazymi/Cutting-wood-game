using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter),typeof(MeshCollider))]
public class MeshDeformer : MonoBehaviour
{
    private Mesh _deformingMesh;
    private Vector3[] _originalVertices;
    private Vector3[] _displacedVertices;
    private Vector3[] _vertexVelocities;
    private MeshCollider _meshCollider;
    
    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _deformingMesh = GetComponent<MeshFilter>().mesh;
        _originalVertices = _deformingMesh.vertices;
        _displacedVertices = new Vector3[_originalVertices.Length];
        for (int i = 0; i < _originalVertices.Length; i++)
        {
            _displacedVertices[i] = _originalVertices[i];
        }

        _vertexVelocities = new Vector3[_originalVertices.Length];
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        _deformingMesh.vertices = _displacedVertices;
        _deformingMesh.RecalculateNormals();
        _meshCollider.sharedMesh = null;
        _meshCollider.sharedMesh = _deformingMesh;
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        var xs = new float[_displacedVertices.Length];
        var ys = new float[_displacedVertices.Length];
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            xs[i] = _displacedVertices[i].x;
            ys[i] = _displacedVertices[i].y;
        }

        var closestX = xs.OrderBy(v => Math.Abs((long) v - point.x)).First();
        var closestY = ys.OrderBy(v => Math.Abs((long) v - point.y)).First();
        SetDeforming(closestX,closestY, force);
    }

    private void SetDeforming(float pointX, float pointY, float force)
    {
        force = force * Time.deltaTime;
        var idCurrentVectors = new List<int>();
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            if (_displacedVertices[i].x == pointX && pointY == _displacedVertices[i].y)
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
        UpdateMesh();
    }
}