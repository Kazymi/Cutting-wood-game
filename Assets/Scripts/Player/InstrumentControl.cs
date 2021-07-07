using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class InstrumentControl : MonoBehaviour
{
    private InputHandler _inputHandler;

    private void Update()
    {
        DragObject(_inputHandler.DeltaPosition);
    }

    public void Initialize(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }
    private void DragObject(Vector3 deltaPosition)
    {
        transform.position += deltaPosition;
    }
}