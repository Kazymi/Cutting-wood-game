using UnityEngine;
using UnityEngine.UI;

public class ComparatorMesh : MonoBehaviour
{
   [SerializeField] private MeshDeformer mainMeshDeformer;
   [SerializeField] private MeshDeformer compareMeshDeformer;

   [SerializeField] private Text textPercent;
   [SerializeField] private Button compareButton;

   private void OnEnable()
   {
      compareButton?.onClick.AddListener(Compare);
   }

   private void OnDisable()
   {
      compareButton?.onClick.RemoveListener(Compare);
   }

   private void Compare()
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
      textPercent.text =(100 - allPercent).ToString();
   }
}
