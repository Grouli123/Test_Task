using System;
using System.Collections.Generic;
using Services.Factories.Enemy;
using Services.Random;
using StaticData.Level;
using UnityEngine;

namespace Enemies.Spawn
{
  public class EnemySpawner : IEnemySpawner
  {
    private readonly IEnemiesFactory enemiesFactory;
    private readonly IRandomService randomService;
    private readonly List<SpawnPoint> spawnPoints = new List<SpawnPoint>(20);

    public event Action<GameObject> Spawned;

    public EnemySpawner(IEnemiesFactory enemiesFactory, IRandomService randomService)
    {
      this.enemiesFactory = enemiesFactory;
      this.randomService = randomService;
    }

    public void Cleanup()
    {
      spawnPoints.Clear();
      enemiesFactory.Cleanup();
    }

    public void AddPoint(SpawnPoint spawnPoint) => 
      spawnPoints.Add(spawnPoint);

    public void Spawn(WaveEnemy[] enemies)
    {
      for (int i = 0; i < enemies.Length; i++)
      {
        SpawnEnemy(enemies[i]);
      }
    }

    private void SpawnEnemy(WaveEnemy waveEnemy)
    {
      for (int i = 0; i < waveEnemy.Count; i++)
      {
        Spawned?.Invoke(SpawnedEnemy(waveEnemy));
      }
      
    }

    private GameObject SpawnedEnemy(WaveEnemy waveEnemy) => 
      enemiesFactory.SpawnMonster(waveEnemy.Id, RandomSpawnPoint().transform, waveEnemy.DamageCoeff, waveEnemy.HpCoeff);

    private SpawnPoint RandomSpawnPoint() => 
      spawnPoints[randomService.Next(spawnPoints.Count)];
  }
}