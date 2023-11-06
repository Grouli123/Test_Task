using System;
using System.Collections.Generic;
using Loots;
using StaticData.Loot.Items;

namespace Services.PlayerData
{
  public class Inventory
  {
    private InventorySlot[] slots;

    public IEnumerable<InventorySlot> Slots => slots;
    
    public event Action Changed;

    public Inventory(int slotCount) => 
      CreateSlots(slotCount);

    public void ReinitSlots(int slotCount)
    {
      CreateSlots(slotCount);
    }

    public bool IsCanAddItem(ItemStaticData item)
    {
      for (int i = 0; i < slots.Length; i++)
      {
        if (IsSlotEmpty(slots[i]) || IsCanBeUnion(item, slots[i]))
          return true;
      }

      return false;
    }

    public void AddItem(ItemStaticData item)
    {
      InventorySlot slot = SameSlot(item);

      if (slot == null)
        slot = EmptySlot();

      slot.PutItem(item);
      NotifyAboutChange();
    }

    public void RemoveItem(ItemStaticData item)
    {
      InventorySlot slot = SameSlot(item);
      
      if (slot == null)
        return;
      
      slot.RemoveItem(1);
      NotifyAboutChange();
    }

    private InventorySlot SameSlot(ItemStaticData item)
    {
      for (int i = 0; i < slots.Length; i++)
      {
        if (IsCanBeUnion(item, slots[i]))
          return slots[i];
      }

      return null;
    }

    private InventorySlot EmptySlot()
    {
      for (int i = 0; i < slots.Length; i++)
      {
        if (IsSlotEmpty(slots[i]))
          return slots[i];
      }

      return null;
    }

    private bool IsCanBeUnion(ItemStaticData item, InventorySlot slot) => 
      IsSlotEmpty(slot) == false && IsSameItem(item, slot.Item) && item.StackableType == StackableType.Stackable;

    private bool IsSameItem(ItemStaticData item, ItemStaticData slotItem) => 
      slotItem.ID == item.ID;

    private bool IsSlotEmpty(InventorySlot slot) => 
      slot.Item == null;

    private void CreateSlots(int slotCount)
    {
      slots = new InventorySlot[slotCount];
      for (int i = 0; i < slotCount; i++)
      {
        slots[i] = new InventorySlot(i);
      }
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke();
  }
}