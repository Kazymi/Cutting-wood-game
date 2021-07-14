using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
   [SerializeField] private List<InstrumentButton> _instrumentButtons;
   [SerializeField] private GameManager gameManager;
   [SerializeField] private Button paintButton;
   [SerializeField] private TMP_Text textPercent;
   [SerializeField] private Button compareButton;

   private void Awake()
   {
      var allInstrument = new List<InstrumentDeformationDealer>();
      foreach (var instrument in _instrumentButtons)
      {
         allInstrument.Add(instrument.InstrumentDeformationDealer);
      }
      gameManager.Initialize(allInstrument);
   }

   private void OnEnable()
   {
      paintButton.onClick.AddListener(() => gameManager.StartDraw());
      compareButton?.onClick.AddListener(Compare);
      foreach (var button in _instrumentButtons)
      {
         button.Button.onClick.AddListener(() => gameManager.EnableInstrument(button.InstrumentDeformationDealer));
      }
   }

   private void OnDisable()
   {
      paintButton.onClick.RemoveListener(() => gameManager.StartDraw());
      compareButton?.onClick.RemoveListener(Compare);
      foreach (var button in _instrumentButtons)
      {
         button.Button.onClick.RemoveListener(() => gameManager.EnableInstrument(button.InstrumentDeformationDealer));
      }
   }

   private void Compare()
   {
      textPercent.text = gameManager.Compare();
   }
}