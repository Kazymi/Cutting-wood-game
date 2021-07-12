using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
   [SerializeField] private List<InstrumentButton> _instrumentButtons;
   private void OnEnable()
   {
      foreach (var button in _instrumentButtons)
      {
         button.Button.onClick.AddListener(() => SetInstrument(button.InstrumentDeformationDealer));
      }
   }

   private void OnDisable()
   {
      foreach (var button in _instrumentButtons)
      {
         button.Button.onClick.RemoveListener(() => SetInstrument(button.InstrumentDeformationDealer));
      }
   }

   private void SetInstrument(InstrumentDeformationDealer instrumentControl)
   {
      DisableAllInstrument();
      instrumentControl.gameObject.SetActive(true);
   }
   private void DisableAllInstrument()
   {
      foreach (var instrument in _instrumentButtons)
      {
         instrument.InstrumentDeformationDealer.gameObject.SetActive(false);
      }
   }
}
