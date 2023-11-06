using System;
using System.Collections.Generic;
using Bonuses;
using Enemies.Spawn;
using Services.Random;
using StaticData.Level;
using UnityEngine;

namespace Services.Bonuses
{
  public class BonusSpawner : IBonusSpawner
  {
    private readonly IBonusFactory bonusFactory;
    private readonly IRandomService randomService;
    private readonly List<SpawnPoint> spawnPoints = new List<SpawnPoint>(20);

    public BonusSpawner(IBonusFactory bonusFactory, IRandomService randomService)
    {
      this.bonusFactory = bonusFactory;
      this.randomService = randomService;
    }

    public void Cleanup()
    {
      spawnPoints.Clear();
      bonusFactory.Cleanup();
    }
      
    
    public void AddPoint(SpawnPoint spawnPoint) => 
      spawnPoints.Add(spawnPoint);

    public void SpawnBonus(WaveBonus waveBonus)
    {
      for (int i = 0; i < waveBonus.Count; i++)
      {
        bonusFactory.SpawnBonus(waveBonus.TypeId, waveBonus.Value, RandomSpawnPoint().transform);
      }
    }

    private SpawnPoint RandomSpawnPoint() => 
      spawnPoints[randomService.Next(spawnPoints.Count)];
  }
}