using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    [SerializeField] private bool unlockDeform;

    private Mesh _deformingMesh;
    private Vector3[] _originalVertices;
    private Vector3[] _displacedVertices;
    private List<CircleVertex> _meshVectorsList;
    private bool _unlockSplit = true;
    private bool _initialized = false;
    public List<CircleVertex> CircleVertex => _meshVectorsList;

    public bool UnlockDeform
    {
        set => unlockDeform = value;
    }

    private void Start()
    {
        if(_initialized) return;
        Initialize();
    }

    public List<float> GetCirclesMagnitudes()
    {
        var magnitudes = new List<float>();
        foreach (var circle in _meshVectorsList)
        {
            magnitudes.Add(circle.Magnitude);
        }

        return magnitudes;
    }
    
    private void Initialize()
    {
        if(_initialized) return;
        _deformingMesh = GetComponent<MeshFilter>().mesh;

        _originalVertices = new Vector3[_deformingMesh.vertices.Length];
        _displacedVertices = new Vector3[_deformingMesh.vertices.Length];
        for (var i = 0; i < _deformingMesh.vertices.Length; i++)
        {
            var vertexWorldPosition = transform.TransformPoint(_deformingMesh.vertices[i]);
            _originalVertices[i] = vertexWorldPosition;
            _displacedVertices[i] = vertexWorldPosition;
        }
        UpdateMesh();
        VectorsInitialize();
    }

    private void Generate(Vector3[] currentVertical, List<float> def)
    {
        Initialize();
        _initialized = true;
        _deformingMesh.vertices = currentVertical;
        StartCoroutine(DeformVertexExceptCurrent(def));
    }
    
    public void Generate(List<float> circleVertices)
    {
        Initialize();
        VectorsInitialize();
        for (int i = 0; i < circleVertices.Count; i++)
        {
            _meshVectorsList[i].Initialize(circleVertices[i]);
        }
        _initialized = true;
        foreach (var circle in _meshVectorsList)
        {
            foreach (var vertex in circle.Vertex)
            {
                _displacedVertices[vertex.ID] = vertex.Vector3;
            }
        }
        UpdateMesh();
    }
    
    private void OnApplicationQuit()
    {
        for (int i = 0; i < _originalVertices.Length; i++)
        {
            _originalVertices[i] = transform.InverseTransformPoint(_originalVertices[i]);
        }
        _deformingMesh.vertices = _originalVertices;
        _deformingMesh.RecalculateNormals();
    }

    private IEnumerator DeformVertexExceptCurrent(List<float> vertices)
    {
        Debug.Log(vertices.Count);
        yield return new WaitForSeconds(0.1f);
        foreach (var circle in vertices)
        {
            var i =GetVerticalByXPosition(circle);
            i.StartDeformation(10000,false);
            foreach (var vertex in i.Vertex)
            {
                _displacedVertices[vertex.ID] = vertex.Vector3;
            }
        }
        UpdateMesh();
    }

    private void VectorsInitialize()
    {
        _meshVectorsList = new List<CircleVertex>();
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            var currentX = Mathf.Round(_displacedVertices[i].x);
            var vertical = GetVerticalByXPosition(currentX);
            if (vertical == null)
            {
                vertical = new CircleVertex(currentX, this);
                _meshVectorsList.Add(vertical);
            }

            vertical.AddVector(_displacedVertices[i], i);
        }
    }

    public CircleVertex GetVerticalByXPosition(float x)
    {
        foreach (var i in _meshVectorsList)
        {
            if (i.XPosition == x) return i;
        }

        return null;
    }

    private void UpdateMesh()
    {
        var vertices = new Vector3[_displacedVertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = transform.InverseTransformPoint(_displacedVertices[i]);
        }

        _deformingMesh.vertices = vertices;
        _deformingMesh.RecalculateNormals();
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        if (unlockDeform == false) return;
        var closestX = (int)point.x;
        SetDeforming(closestX, force);
    }

    private void SetDeforming(float pointX, float force)
    {
        var meshVector = GetVerticalByXPosition(pointX);
        if (meshVector == null) return;
        meshVector.StartDeformation(force,true);
        foreach (var i in meshVector.Vertex)
        {
            _displacedVertices[i.ID] = i.Vector3;
        }
        UpdateMesh();
    }

    public void SetDeforming(CircleVertex circleVertex, float force)
    {
        if(unlockDeform == false) return;
        circleVertex.StartDeformation(force,true);
        foreach (var i in circleVertex.Vertex)
        {
            _displacedVertices[i.ID] = i.Vector3;
        }
        UpdateMesh();
    }

    public void SplitMesh(float PointX)
    {
        if (_unlockSplit == false) return;
        var firstMesh = new List<float>();
        var secondMesh = new List<float>();
        foreach (var i in _meshVectorsList)
        {
            if (i.XPosition >= PointX)
            {
                if(i.Magnitude > 0) firstMesh.Add(i.XPosition);
            }
            else
            {
                if(i.Magnitude > 0) secondMesh.Add(i.XPosition);
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
        StartCoroutine(Timer());
    }

    private void Split(List<float> splitCirclesId)
    {
        GameObject newGameObject = Instantiate(gameObject);
        var i = newGameObject.GetComponent<MeshDeformer>();
        i.Generate(_displacedVertices,splitCirclesId);
        newGameObject.AddComponent<Rigidbody>();
        Destroy(newGameObject,4f);
        i.unlockDeform = false;
    }

    private void Splited(List<float> splitCirclesId)
    {
        StartCoroutine(DeformVertexExceptCurrent(splitCirclesId));
    }

    private IEnumerator Timer()
    {
        _unlockSplit = false;
        yield return new WaitForSeconds(1f);
        _unlockSplit = true;
    }

    private void OnDrawGizmos()
    {
        if (_displacedVertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            Gizmos.DrawSphere(_displacedVertices[i], 0.1f);
        }
    }
}