using System.Collections;
using Bootstrapp;
using Enemies.Entity;
using Enemies.Spawn;
using Services.Bonuses;
using StaticData.Level;
using UnityEngine;

namespace Services.Waves
{
  public class WaveServices : IWaveServices
  {
    private readonly IEnemySpawner enemiesSpawner;
    private readonly ICoroutineRunner coroutineRunner;
    private readonly IBonusSpawner bonusSpawner;
    private LevelWaveStaticData waves;

    private int currentEnemiesCount;
    private int currentWaveIndex;

    public WaveServices(IEnemySpawner spawner, ICoroutineRunner coroutineRunner, IBonusSpawner bonusSpawner)
    {
      enemiesSpawner = spawner;
      this.coroutineRunner = coroutineRunner;
      this.bonusSpawner = bonusSpawner;
      enemiesSpawner.Spawned += OnEnemySpawned;
    }

    public void Cleanup() => 
      enemiesSpawner.Spawned -= OnEnemySpawned;

    public void Start()
    {
      currentWaveIndex = 0;
      coroutineRunner.StartCoroutine(StartWave(waves.FirstWaveDelay));
    }
    
    public void SetLevelWaves(LevelWaveStaticData wavesData) => 
      waves = wavesData;

    private void OnEnemySpawned(GameObject enemy) => 
      enemy.GetComponent<EnemyDeath>().Happened += OnEnemyDead;

    private void OnEnemyDead(EnemyTypeId enemyTypeId, GameObject enemy)
    {
      enemy.GetComponent<EnemyDeath>().Happened -= OnEnemyDead;
      currentEnemiesCount--;
      if (currentEnemiesCount <= 0)
        CompleteWave();
    }

    private void CompleteWave()
    {
      coroutineRunner.StartCoroutine(StartWave(waves.Waves[currentWaveIndex].WaveWaitTime));
      SpawnBonuses();
      IncWaveIndex();
    }

    private IEnumerator StartWave(float delay)
    {
      yield return new WaitForSeconds(delay);
      
      enemiesSpawner.Spawn(waves.Waves[currentWaveIndex].Enemies);
      currentEnemiesCount = 0;
      for (int i = 0; i < waves.Waves[currentWaveIndex].Enemies.Length; i++)
      {
        currentEnemiesCount += waves.Waves[currentWaveIndex].Enemies[i].Count;
      }
    }

    private void SpawnBonuses()
    {
      WaveBonus[] bonuses = waves.Waves[currentWaveIndex].Bonuses;
      for (int i = 0; i <bonuses.Length ; i++)
      {
        bonusSpawner.SpawnBonus(bonuses[i]);
      }
    }

    private void IncWaveIndex()
    {
      currentWaveIndex++;
      currentWaveIndex = Mathf.Clamp(currentWaveIndex, 0, waves.Waves.Length);
    }
  }
}