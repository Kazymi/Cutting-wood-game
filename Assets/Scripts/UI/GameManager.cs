using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MeshDeformer mainMeshDeformer;
    [SerializeField] private MeshDeformer compareMeshDeformer;
    [SerializeField] private DrawInstument drawInstrument;

    private List<InstrumentDeformationDealer> _instrumentDeformationDealers;

    public void Initialize(List<InstrumentDeformationDealer> instrumentDeformationDealers)
    {
        _instrumentDeformationDealers = instrumentDeformationDealers;
        foreach (var insDeformator in instrumentDeformationDealers)
        {
            insDeformator.Initialize(mainMeshDeformer, compareMeshDeformer);
        }
        DisableAllInstrument();
    }

    public void EnableInstrument(InstrumentDeformationDealer instrumentDeformationDealer)
    {
        DisableAllInstrument();
        instrumentDeformationDealer.gameObject.SetActive(true);
    }

    public void StartDraw()
    {
        DisableAllInstrument();
        compareMeshDeformer.gameObject.SetActive(false);
        drawInstrument.UnlockDrawing = true;
    }

    private void DisableAllInstrument()
    {
        foreach (var instrument in _instrumentDeformationDealers)
        {
            instrument.gameObject.SetActive(false);
        }
    }
    public string Compare()
    {
        var mainMagnitudes = mainMeshDeformer.GetCirclesMagnitudes();
        var compareMagnitudes = compareMeshDeformer.GetCirclesMagnitudes();
        float allPercent = 0;
        mainMeshDeformer.UnlockDeform = false;
      
        for (var index = 0; index < compareMagnitudes.Count; index++)
        {
            var comparePercent = mainMagnitudes[index] > compareMagnitudes[index]
                ? (mainMagnitudes[index] - compareMagnitudes[index]) / compareMagnitudes[index] * 100
                : (compareMagnitudes[index] - mainMagnitudes[index]) / compareMagnitudes[index] * 100;
            allPercent+=comparePercent;
        }

        allPercent /= compareMagnitudes.Count;
        return (100 - allPercent).ToString();
    }
}