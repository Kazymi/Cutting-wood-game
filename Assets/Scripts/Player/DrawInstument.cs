using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DrawInstument : MonoBehaviour
{
    [SerializeField] private int pixelsSize = 10;
    [SerializeField] private FlexibleColorPicker _flexibleColorPicker;
    [SerializeField] private bool unlockDrawing;
    private Camera _cam;

    public bool UnlockDrawing
    {
        set => unlockDrawing = value;
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) == false || unlockDrawing == false)
            return;

        RaycastHit hit;
        if (!Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        var draw = hit.transform.GetComponent<Drawing>();
        if (draw)
        {
            draw.Draw(hit, _flexibleColorPicker.color, pixelsSize);
        }
    }
}