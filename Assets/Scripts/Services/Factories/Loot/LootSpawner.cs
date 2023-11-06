using System.Collections;
using System.Collections.Generic;
using ConstantsValue;
using Interfaces;
using Loots;
using Services.Assets;
using StaticData.Loot.Items;
using UnityEngine;

namespace Services.Factories.Loot
{
  public class LootSpawner : ILootSpawner
  {
    private readonly IAssetProvider assetProvider;

    private readonly Queue<DroppedLoot> droppedLoots;

    public LootSpawner(IAssetProvider assetProvider)
    {
      this.assetProvider = assetProvider;
      droppedLoots = new Queue<DroppedLoot>(30);
    }

    public void Cleanup()
    {
      DroppedLoot loot;
      while (droppedLoots.Count > 0)
      {
        loot = droppedLoots.Dequeue();
        loot.PickedUp -= OnLootPickedUp;
      }
    }

    public void SpawnLoot(ItemStaticData droppedLoot, Vector3 position)
    {
      DroppedLoot loot = droppedLoots.Count > 0 ? droppedLoots.Dequeue() : CreateLoot();
      loot.SetPosition(position);
      loot.SetItem(droppedLoot);
      loot.Show();
    }

    private DroppedLoot CreateLoot()
    {
      DroppedLoot loot = assetProvider.Instantiate<DroppedLoot>(AssetsPath.DroppedLootPrefabPath);
      loot.Hide();
      loot.PickedUp += OnLootPickedUp;
      return loot;
    }

    private void OnLootPickedUp(DroppedLoot loot)
    {
      loot.Hide();
      droppedLoots.Enqueue(loot);
    }
  }
}