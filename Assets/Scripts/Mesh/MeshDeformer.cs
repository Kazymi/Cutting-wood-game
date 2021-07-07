using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class MeshDeformer : MonoBehaviour
{
    [SerializeField] private GameObject splitCube;
    [SerializeField] private bool unlockDeform;
    [SerializeField] private CubeMain cubeMain;

    private Mesh _deformingMesh;
    private Vector3[] _originalVertices;
    private Vector3[] _displacedVertices;
    private MeshCollider _meshCollider;
    private List<СircleVertex> _meshVectorsList;
    private bool _unlockSplit = true;

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

        UpdateMesh();
        VectorsInitialize();
    }

    public IEnumerator DeformVertexExceptCurrent(List<int> vertices)
    {
        yield return new WaitForSeconds(0.1f);
        var deformVertexes = new List<Vertical>();
        foreach (var circle in _meshVectorsList)
        {
            foreach (var idVertex in circle.Vertex)
            {
                if (vertices.Contains(idVertex.ID))
                {
                    deformVertexes.Add(idVertex);
                }
            }
        }

        foreach (var vertex in deformVertexes)
        {
            vertex.Deformation(10000, false);
            _displacedVertices[vertex.ID] = vertex.Vector3;
        }

        UpdateMesh();
    }

    private void VectorsInitialize()
    {
        _meshVectorsList = new List<СircleVertex>();
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            var vertical = GetVerticalByXPosition(_displacedVertices[i].x);
            if (vertical == null)
            {
                vertical = new СircleVertex(_displacedVertices[i].x, transform.position, this);
                _meshVectorsList.Add(vertical);
            }

            vertical.AddVector(_displacedVertices[i], i, cubeMain.Scale);
        }
    }

    private СircleVertex GetVerticalByXPosition(float x)
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
        if (unlockDeform == false) return;
        var closestX = (int) point.x;
        SetDeforming(closestX, force);
    }

    private void SetDeforming(float pointX, float force)
    {
        force = force * Time.deltaTime;
        var meshVector = GetVerticalByXPosition(pointX);
        if (meshVector == null) return;
        meshVector.StartDeformation(force);
        foreach (var i in meshVector.Vertex)
        {
            _displacedVertices[i.ID] = i.Vector3;
        }

        UpdateMesh();
    }

    public void SplitMesh(float PointX)
    {
        var firstMesh = new List<Vertical>();
        var secondMesh = new List<Vertical>();
        foreach (var i in _meshVectorsList)
        {
            if (i.XPosition >= PointX)
            {
                foreach (var vertex in i.Vertex)
                {
                    if (vertex.Magnitude > 0)
                        firstMesh.Add(vertex);
                }
            }
            else
            {
                foreach (var vertex in i.Vertex)
                {
                    if (vertex.Magnitude > 0)
                        secondMesh.Add(vertex);
                }
            }
        }

        if (firstMesh.Count > secondMesh.Count)
        {
            Split(firstMesh);
            Splited(secondMesh);
        }
        else
        {
            Split(secondMesh);
            Splited(firstMesh);
        }
    }

    private void Split(List<Vertical> splitCirclesId)
    {
        if (_unlockSplit == false) return;
        GameObject newGameObject = Instantiate(gameObject);
        var i = newGameObject.GetComponent<CubeMain>();
        i.Generate(splitCirclesId,_displacedVertices);
        StartCoroutine(Timer());
    }

    private void Splited(List<Vertical> splitCirclesId)
    {
        List<int> Allverticals = new List<int>();
        foreach (var i in splitCirclesId)
        {
            Allverticals.Add(i.ID);
        }

        StartCoroutine(DeformVertexExceptCurrent(Allverticals));
    }

    private IEnumerator Timer()
    {
        _unlockSplit = false;
        yield return new WaitForSeconds(1f);
        _unlockSplit = true;
    }
}