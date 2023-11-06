using StaticData.Loot.Items;

namespace Services.PlayerData
{
  public class InventorySlot
  {
    public ItemStaticData Item;
    public int Count;
    public int Index;

    public InventorySlot(int index)
    {
      Index = index;
    }

    public void ClearSlot()
    {
      Item = null;
      Count = 0;
    }

    public void RemoveItem(int count)
    {
      Count -= count;
      if (Count == 0)
        ClearSlot();
    }

    public void PutItem(ItemStaticData item)
    {
      if (Item == null)
        Item = item;

      Count++;
    }
  }
}