using Enemies.Entity;
using Enemies.Spawn;
using Loots;
using Services.Factories.Loot;
using Services.Random;
using Services.StaticData;
using StaticData.Loot;
using StaticData.Loot.Items;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.Loot
{
  public class LootService : ILootService
  {
    private readonly ILootSpawner lootSpawner;
    private readonly IRandomService randomService;
    private readonly IStaticDataService staticDataService;
    private readonly IEnemySpawner enemySpawner;
    private readonly LootContainer lootContainer;

    private string levelName; 

    public LootService(ILootSpawner lootSpawner, IRandomService randomService, IStaticDataService staticDataService, IEnemySpawner enemySpawner, LootContainer lootContainer)
    {
      this.lootSpawner = lootSpawner;
      this.randomService = randomService;
      this.staticDataService = staticDataService;
      this.enemySpawner = enemySpawner;
      this.lootContainer = lootContainer;
      this.enemySpawner.Spawned += SubscribeForEnemy;
    }

    public void Cleanup() => 
      enemySpawner.Spawned -= SubscribeForEnemy;

    public void SetSceneName(string name) => 
      levelName = name;

    private void SubscribeForEnemy(GameObject enemy) => 
      enemy.GetComponent<EnemyDeath>().Happened += OnEnemyDeath;

    private void OnEnemyDeath(EnemyTypeId enemyTypeId, GameObject enemyObject)
    {
      enemyObject.GetComponent<EnemyDeath>().Happened -= OnEnemyDeath;
      CreateLoot(enemyTypeId, enemyObject.transform.position);
    }

    private void CreateLoot(EnemyTypeId enemyTypeId, Vector3 position)
    {
      EnemyLoot loot = staticDataService.ForLoot(levelName, enemyTypeId);
      
      SpawnLoot(loot, position);
    }

    private void SpawnLoot(EnemyLoot loot, Vector3 position)
    {
      int lootCount = randomService.Next(loot.LootCountRange.x, loot.LootCountRange.y);
      for (int i = 0; i < lootCount; i++)
      {
        ItemStaticData lootData = DroppedLoot(loot);
        if (lootData != null)
          lootSpawner.SpawnLoot(lootData, position);  
      }
    }

    private ItemStaticData DroppedLoot(EnemyLoot enemyLoot)
    {
      float randomValue = randomService.NextFloat();
      for (int i = 0; i < enemyLoot.RareTypeDrops.Length; i++)
      {
        if (randomValue < enemyLoot.RareTypeDrops[i].Chance)
          return RandomLoot(lootContainer.RareTypeItems(enemyLoot.RareTypeDrops[i].RareType));
        randomValue -= enemyLoot.RareTypeDrops[i].Chance;
      }

      return null;
    }

    private ItemStaticData RandomLoot(ItemStaticData[] items)
    {
      if (items == null || items.Length == 0)
        return null;

      return items[randomService.Next(0, items.Length)];
    }
  }
}