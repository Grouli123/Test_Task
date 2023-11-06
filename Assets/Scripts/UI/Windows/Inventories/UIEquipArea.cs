using System;
using System.Collections.Generic;
using Services.PlayerData;
using StaticData.Loot.Items;
using StaticData.UI.EquipmentArea;
using UnityEngine;

namespace UI.Windows.Inventories
{
  public class UIEquipArea : MonoBehaviour
  {
    [SerializeField] private UIEquipSlot[] slots;
    [SerializeField] private EquipmentAreaStaticData areaData;

    public ItemStaticData LastClickedItem { get; private set; }
    
    public event Action<ItemStaticData> EquipClicked;

    public void InitSlots(IEnumerable<EquipmentSlot> equipmentSlots)
    {
      int index = 0;
      foreach (EquipmentSlot equipmentSlot in equipmentSlots)
      {
        slots[index].SetEquipSlot(areaData.GetSlotView(equipmentSlot.LootType), equipmentSlot.LootType);
        slots[index].SetItem(equipmentSlot.Item);
        slots[index].Clicked += NotifyAboutEquipClick;
        index++;
      }
    }

    public void Cleanup()
    {
      for (int i = 0; i < slots.Length; i++)
      {
        slots[i].Clicked -= NotifyAboutEquipClick;
      }
    }

    public void RemoveLastClickedItem() => 
      LastClickedItem = null;

    public void UpdateView(IEnumerable<EquipmentSlot> equipmentSlots)
    {
      int index = 0;
      foreach (EquipmentSlot equipmentSlot in equipmentSlots)
      {
        slots[index].SetItem(equipmentSlot.Item);
        index++;
      }
    }

    private void NotifyAboutEquipClick(ItemStaticData item)
    {
      if (item != null)
      {
        LastClickedItem = item;
        EquipClicked?.Invoke(item);
      }
    }
  }
}