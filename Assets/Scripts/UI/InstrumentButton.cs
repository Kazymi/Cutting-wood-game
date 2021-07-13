using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InstrumentButton
{
    [SerializeField] private InstrumentDeformationDealer _instrumentDeformationDealer;
    [SerializeField] private Button instrumentButton;

    public InstrumentDeformationDealer InstrumentDeformationDealer => _instrumentDeformationDealer;
    public Button Button => instrumentButton;
}
