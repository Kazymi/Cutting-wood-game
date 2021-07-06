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
    private List<MeshVectors> _meshVectorsList;
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
        VectorsInitialize();
    }

    private void VectorsInitialize()
    {
        _meshVectorsList = new List<MeshVectors>();
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            var vertical = GetVerticalByXPosition(_displacedVertices[i].x);
            if (vertical == null)
            {
                vertical = new MeshVectors(_displacedVertices[i].x);
                _meshVectorsList.Add(vertical);
            }
            vertical.AddVector(_displacedVertices[i],i);
        }

        foreach (var i in _meshVectorsList)
        {
          i.UpdateState();
        }
    }

    private MeshVectors GetVerticalByXPosition(float x)
    {
        foreach (var i in _meshVectorsList)
        {
            if (i.XPosition == x) return i;
        }
        return null;
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
        var xs = new List<float>();
        foreach (var i in _meshVectorsList)
        {
            xs.Add(i.XPosition);
        }
        var closestX = xs.OrderBy(v => Math.Abs((long) v - point.x)).First();
        SetDeforming(closestX, force);
    }

    private void SetDeforming(float pointX, float force)
    {
        force = force * Time.deltaTime;
        var idCurrentVectors = new List<int>();
        var meshVector = GetVerticalByXPosition(pointX);
        if(meshVector == null) return;

        foreach (var i in meshVector.Vectors)
        {
            idCurrentVectors.Add(i.ID);
        }

        var meanValue = meshVector.maxY / 2;
        foreach (var i in idCurrentVectors)
        {
            if (_displacedVertices[i].y == meshVector.maxY || _displacedVertices[i].y > meanValue) _displacedVertices[i].y -= force;
            if (_displacedVertices[i].y == meshVector.minY || _displacedVertices[i].y < meanValue) _displacedVertices[i].y += force;
        }

        foreach (var i in meshVector.Vectors)
        {
            i.Vector3 = _displacedVertices[i.ID];
        }
        UpdateMesh();
    }
}