using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentDeformationDealer : MonoBehaviour
{
    [SerializeField] private DamageDealerConfiguration damageDealerConfiguration;
    [SerializeField] private bool unlockPolishing;
    [SerializeField] private PolishingSlider polishingSlider;

    private float _currentPolishingValue;
    private MeshDeformer _firshMeshDeformer;
    private MeshDeformer _SecondMeshDeformer;
    private List<float> _xPositions = new List<float>();

    public float CurrentPolishingValueBySlider => _currentPolishingValue/damageDealerConfiguration.MaxPolishing;
    private void Start()
    {
        StartCoroutine(GenerateRadius());
    }

    private void Update()
    {
        CheckRadiusX();
    }
    
    public void Initialize(MeshDeformer firstMeshDeformer, MeshDeformer secondMeshDeformer)
    {
        _firshMeshDeformer = firstMeshDeformer;
        _SecondMeshDeformer = secondMeshDeformer;
        _currentPolishingValue = damageDealerConfiguration.MaxPolishing;
        polishingSlider?.Initialize(this);
        polishingSlider?.UpdatePolishing();
    }
    
    private IEnumerator GenerateRadius()
    {
        yield return new WaitForEndOfFrame();
        foreach (var i in _firshMeshDeformer.CircleVertex)
        {
            _xPositions.Add(i.XPosition);
        }
    }
    
    private void Polishing(CircleVertex circleVertexMain, CircleVertex ComparableCircle)
    {
        if(_currentPolishingValue <= 0) return;
        var magnitude = circleVertexMain.Magnitude;
        if (magnitude > ComparableCircle.Magnitude)
        {
            magnitude -= damageDealerConfiguration.DamagePolishing * Time.deltaTime;
            if (magnitude < ComparableCircle.Magnitude) magnitude = ComparableCircle.Magnitude;
        }
        else
        {
            magnitude += damageDealerConfiguration.DamagePolishing * Time.deltaTime;
            if (magnitude > ComparableCircle.Magnitude) magnitude = ComparableCircle.Magnitude;
        }
        _firshMeshDeformer.Polishing(circleVertexMain,magnitude);
        polishingSlider?.UpdatePolishing();
    }

    private void CheckRadiusX()
    {
        var circleInRadius = new List<CircleVertex>();
        var animationCurveX = damageDealerConfiguration.AnimationCurve
            .keys[damageDealerConfiguration.AnimationCurve.length - 1].value;
        
        var minX = animationCurveX + transform.position.x;
        var maxX = transform.position.x - animationCurveX;
        
        foreach (var _xPosition in _xPositions)
        {
            if (_xPosition > maxX && _xPosition < minX)
            {
                var i = _firshMeshDeformer.GetVerticalByXPosition(_xPosition);
                if (CheckRadiusY(i, _xPosition))
                {
                    circleInRadius.Add(i);
                }
            }
        }

        if (unlockPolishing && circleInRadius.Count != 0)
            _currentPolishingValue -= damageDealerConfiguration.ReductionPolishing*Time.deltaTime;
        foreach (var i in circleInRadius)
        {
            if (unlockPolishing == false)
                _firshMeshDeformer.SetDeforming(i, damageDealerConfiguration.Damage);
            else Polishing(i, _SecondMeshDeformer.GetVerticalByXPosition(i.XPosition));
        }
    }

    private bool CheckRadiusY(CircleVertex circleVertex, float X)
    {
        var pos = transform.position.x - X;
        var y = transform.position.y + damageDealerConfiguration.AnimationCurve.Evaluate(Math.Abs(pos));
        var CicrcleY = 0 - circleVertex.Magnitude;
        Debug.Log(CicrcleY);
        if (CicrcleY < y) return true;
        return false;
    }
}