using Loots;
using StaticData.Loot.Items;

namespace Services.PlayerData
{
  public class EquipmentSlot
  {
    public readonly LootType LootType;
    public ItemStaticData Item;

    public EquipmentSlot(LootType lootType)
    {
      LootType = lootType;
      Item = null;
    }

    public void PutItem(ItemStaticData item) => 
      Item = item;

    public void RemoveItem() => 
      Item = null;
  }
}