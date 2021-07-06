using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class InstrumentControl : MonoBehaviour
{
    [SerializeField] private float horizontalLimit = 2.5f;
    [SerializeField] private float verticalLimit = 2.5f;
    [SerializeField] private float dragSpeed = 0.1f;

    private Transform _cashedTransform;
    private Vector3 _startingPos;
    private InputHandler _inputHandler;

    private void Start()
    {
        _cashedTransform = transform;
        _startingPos = _cashedTransform.position;
    }

    private void Update()
    {
        DragObject(_inputHandler.DeltaPosition);
    }

    public void Initialize(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }
    private void DragObject(Vector2 deltaPosition)
    {
        Camera cam = Camera.main;
        transform.position = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.0001f);
    }
}