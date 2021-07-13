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
        drawInstrument.UnlockDrawing = true;
    }

    private void DisableAllInstrument()
    {
        foreach (var instrument in _instrumentDeformationDealers)
        {
            instrument.gameObject.SetActive(false);
        }
    }
}