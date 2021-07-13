using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPicker : MonoBehaviour
{
    [SerializeField] private List<Picker> Pickers = new List<Picker>();

    private void OnEnable()
    {
        foreach (var piker in Pickers)
        {
            piker.OpenCanvasButton?.onClick.AddListener(() => EnableCanvas(piker.Canvas));
        }
    } 
    
    private void OnDisable()
    {
        foreach (var piker in Pickers)
        {
            piker.OpenCanvasButton?.onClick.RemoveListener(() => EnableCanvas(piker.Canvas));
        }
    }

    private void EnableCanvas(Canvas canvas)
    {
        DisableAllCanvas();
        canvas.enabled = true;
    }
    
    private void DisableAllCanvas()
    {
        foreach (var piker in Pickers)
        {
            piker.Canvas.enabled = false;
        }
    }
}