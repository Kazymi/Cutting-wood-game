using System;
using UnityEngine;
using UnityEngine.UI;

public class PolishingSlider : MonoBehaviour
{
   [SerializeField] private Slider polishingSlider;
   
   private InstrumentDeformationDealer _instrumentDeformationDealer;

   public void Initialize(InstrumentDeformationDealer instrumentDeformationDealer)
   {
      _instrumentDeformationDealer = instrumentDeformationDealer;
   }
   public void UpdatePolishing()
   {
      polishingSlider.value = _instrumentDeformationDealer.CurrentPolishingValueBySlider;
   }
}
