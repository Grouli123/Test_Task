using System;
using Loots;
using TMPro;
using UnityEngine;

namespace UI.Windows.Inventories
{
  public class InventoryItemTip : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI staminaText;

    public virtual void RemoveTip()
    {
      SetText(itemNameText, "");
      SetText(healthText, "");
      SetText(strengthText, "");
      SetText(staminaText, "");
    }

    public void DisplayTip(string itemName, Characteristic[] characteristics)
    {
      SetText(itemNameText, itemName);
      for (int i = 0; i < characteristics.Length; i++)
      {
        SetText(CharacteristicText(characteristics[i].Type), characteristics[i].Value.ToString());
      }
    }

    private TextMeshProUGUI CharacteristicText(CharacteristicType type)
    {
      switch (type)
      {
        case CharacteristicType.Strength:
          return strengthText;
        case CharacteristicType.Health:
          return healthText;
        case CharacteristicType.Stamina:
          return staminaText;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
    }


    protected void SetText(TextMeshProUGUI tmpText, string text) => 
      tmpText.text = text;
  }
}