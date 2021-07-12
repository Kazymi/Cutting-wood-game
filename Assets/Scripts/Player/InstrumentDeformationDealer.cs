using System;
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
        var minX = damageDealerConfiguration.AnimationCurve.keys[damageDealerConfiguration.AnimationCurve.length-1].value+transform.position.x;
        var maxX = transform.position.x - damageDealerConfiguration.AnimationCurve.keys[damageDealerConfiguration.AnimationCurve.length-1].value;
        foreach (var _xPosition in _xPositions)
        {
            if (_xPosition > maxX && _xPosition < minX)
            {
                Debug.Log($" {maxX} + {minX}");
                var i = meshDeformer.GetVerticalByXPosition(_xPosition);
                if (CheckRadiusY(i,_xPosition))
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

    private bool CheckRadiusY(CircleVertex circleVertex, float X)
    {
        var pos = transform.position.x - X;
        var y = transform.position.y + damageDealerConfiguration.AnimationCurve.Evaluate(Math.Abs(pos));
        foreach (var vertex in circleVertex.Vertex)
        {
            if (vertex.Vector3.y < y)
            {
                Debug.Log(vertex.Vector3 +$" Y{y} X{X}");
                return true;
            }
        }
        return false;
    }
}