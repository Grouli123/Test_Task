using Services.PlayerData;
using StaticData.Loot.Items;
using UnityEngine;

namespace Hero
{
  public class HeroInventory : MonoBehaviour
  {
    private Inventory inventory;

    public void Construct(Inventory inventory)
    {
      this.inventory = inventory;
    }

    public bool IsCanAddItem(ItemStaticData item) => 
      inventory.IsCanAddItem(item);

    public void AddItem(ItemStaticData item) => 
      inventory.AddItem(item);
  }
}