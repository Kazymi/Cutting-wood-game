using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentDeformationDealer : MonoBehaviour
{
    [SerializeField] private DamageDealerConfiguration damageDealerConfiguration;
    [SerializeField] private MeshDeformer meshDeformer;

    private List<float> _xPositions = new List<float>();

    private void Start()
    {
        StartCoroutine(GenerateRadius());
    }

    private IEnumerator GenerateRadius()
    {
        yield return new WaitForEndOfFrame();
        foreach (var i in meshDeformer.CircleVertex)
        {
            _xPositions.Add(i.XPosition);
        }
    }

    private void Update()
    {
        CheckRadiusX();
    }

    private void CheckRadiusX()
    {
        var circleInRadius = new List<CircleVertex>();
        var minX = transform.position.x - damageDealerConfiguration.Radius / 2;
        var maxX = transform.position.x + damageDealerConfiguration.Radius / 2;
        foreach (var _xPosition in _xPositions)
        {
            if (_xPosition < maxX && _xPosition > minX)
            {
                var i = meshDeformer.GetVerticalByXPosition(_xPosition);
                if (CheckRadiusY(i))
                {
                    circleInRadius.Add(i);
                }
            }
        }
        foreach (var i in circleInRadius)
        {
            meshDeformer.SetDeforming(i,damageDealerConfiguration.Damage);
        }
    }

    private bool CheckRadiusY(CircleVertex circleVertex)
    {
        var minY = transform.position.y - damageDealerConfiguration.Radius / 2;
        var maxY = transform.position.y + damageDealerConfiguration.Radius / 2;
        foreach (var vertex in circleVertex.Vertex)
        {
            if (vertex.Vector3.y < maxY && vertex.Vector3.y > minY)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageDealerConfiguration.Radius);
    }
}