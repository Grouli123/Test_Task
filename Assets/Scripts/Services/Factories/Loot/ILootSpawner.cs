using StaticData.Loot.Items;
using UnityEngine;

namespace Services.Factories.Loot
{
  public interface ILootSpawner : ICleanupService
  {
    void SpawnLoot(ItemStaticData droppedLoot, Vector3 position);
  }
}