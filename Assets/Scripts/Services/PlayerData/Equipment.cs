using System;
using System.Collections.Generic;
using Loots;
using StaticData.Loot.Items;

namespace Services.PlayerData
{
  public class Equipment
  {
    private readonly PlayerCharacteristics playerCharacteristics;

    private EquipmentSlot[] equipmentSlots;

    public IEnumerable<EquipmentSlot> Slots => equipmentSlots;

    public event Action Changed;

    public Equipment(PlayerCharacteristics playerCharacteristics, LootType[] slots)
    {
      this.playerCharacteristics = playerCharacteristics;
      CreateSlots(slots);
    }

    public void ReinitSlots(LootType[] slots) => 
      CreateSlots(slots);

    public bool IsCanEquipItem(ItemStaticData item)
    {
      for (int i = 0; i < equipmentSlots.Length; i++)
      {
        if (IsSameEmptyItemSlot(item, equipmentSlots[i]))
          return true;
      }
      return false;
    }

    public void EquipItem(ItemStaticData item)
    {
      for (int i = 0; i < equipmentSlots.Length; i++)
      {
        if (IsSameEmptyItemSlot(item, equipmentSlots[i]))
        {
          equipmentSlots[i].PutItem(item);
          IncCharacteristics(item.Characteristics);
          NotifyAboutChange();
          break;
        }
      }
    }

    public void RemoveItem(ItemStaticData item)
    {
      for (int i = 0; i < equipmentSlots.Length; i++)
      {
        if (IsSameItem(item, equipmentSlots[i]))
        {
          equipmentSlots[i].RemoveItem();
          ReduceCharacteristics(item.Characteristics);
          NotifyAboutChange();
          break;
        }
      }
    }

    private void ReduceCharacteristics(Characteristic[] itemCharacteristics)
    {
      for (int i = 0; i < itemCharacteristics.Length; i++)
      {
        playerCharacteristics.ReduceCharacteristic(itemCharacteristics[i].Type, itemCharacteristics[i].Value);
      }
    }

    private void IncCharacteristics(Characteristic[] itemCharacteristics)
    {
      for (int i = 0; i < itemCharacteristics.Length; i++)
      {
        playerCharacteristics.IncCharacteristic(itemCharacteristics[i].Type, itemCharacteristics[i].Value);
      }
    }

    private bool IsSameItem(ItemStaticData item, EquipmentSlot slot) => 
      slot.Item != null && slot.Item == item;

    private bool IsSameEmptyItemSlot(ItemStaticData item, EquipmentSlot slot) => 
      slot.Item == null && slot.LootType == item.Type;

    private void CreateSlots(LootType[] slots)
    {
      equipmentSlots = new EquipmentSlot[slots.Length];
      for (int i = 0; i < slots.Length; i++)
      {
        equipmentSlots[i] = new EquipmentSlot(slots[i]);
      }
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke();
  }
}