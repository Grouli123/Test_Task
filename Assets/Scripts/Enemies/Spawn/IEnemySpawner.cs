using System;
using Services;
using StaticData.Level;
using UnityEngine;

namespace Enemies.Spawn
{
  public interface IEnemySpawner : ICleanupService
  {
    event Action<GameObject> Spawned;
    void AddPoint(SpawnPoint spawnPoint);
    void Spawn(WaveEnemy[] enemies);
  }
}