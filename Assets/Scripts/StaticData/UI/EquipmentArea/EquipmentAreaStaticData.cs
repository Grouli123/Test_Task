using Loots;
using UnityEngine;

namespace StaticData.UI.EquipmentArea
{
  [CreateAssetMenu(fileName = "EquipmentAreaStaticData", menuName = "Static Data/UI/Create Equipment Area Data", order = 55)]
  public class EquipmentAreaStaticData : ScriptableObject
  {
    public EquipmentAreaSlotView[] SlotViews;

    public Sprite GetSlotView(LootType type)
    {
      for (int i = 0; i < SlotViews.Length; i++)
      {
        if (SlotViews[i].Type == type)
          return SlotViews[i].View;
      }

      return null;
    }
  }
}