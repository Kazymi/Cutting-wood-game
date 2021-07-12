using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMeshControl : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private MeshDeformer meshDeformer;
    [SerializeField] private SaveSystem saveSystem;

    [SerializeField] private bool load;

    private void OnEnable()
    {
        if(saveButton != null) saveButton.onClick.AddListener(Save);
        if(deleteButton != null) deleteButton.onClick.AddListener(Delete);
    }

    private void OnDisable()
    {
        if(saveButton != null) saveButton.onClick.RemoveListener(Save);
        if(deleteButton != null) deleteButton.onClick.RemoveListener(Delete);
    }

    private void Awake()
    {
        if (load)
        {
            meshDeformer.Generate(saveSystem.GetRandomSavedCircle());
        }
    }

    private void Save()
    {
        var magnitudes = new List<float>();
        var circle = meshDeformer.CircleVertex;
        foreach (var VARIABLE in circle)
        {
            magnitudes.Add(VARIABLE.Magnitude);
        }
        saveSystem.SaveNewCircle(magnitudes);
    } 
    private void Delete()
    {
        saveSystem.DeleteAllCircle();
    }
}
