using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Picker
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button openCanvasButton;

    public Canvas Canvas => canvas;
    public Button OpenCanvasButton => openCanvasButton;
}