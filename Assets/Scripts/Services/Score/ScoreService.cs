using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstantsValue;
using Enemies.Entity;
using Enemies.Spawn;
using Services.PlayerData;
using StaticData.Score;
using UnityEngine;

namespace Services.Score
{
  public class ScoreService : IScoreService
  {
    private readonly IEnemySpawner enemySpawner;
    private readonly ScoreStaticData scoreStaticData;
    private readonly PlayerScore playerScore;

    public ScoreService(IEnemySpawner enemySpawner, ScoreStaticData scoreStaticData, PlayerScore playerScore)
    {
      this.enemySpawner = enemySpawner;
      this.scoreStaticData = scoreStaticData;
      this.playerScore = playerScore;
      this.enemySpawner.Spawned += OnEnemySpawned;
    }

    public void Cleanup()
    {
      enemySpawner.Spawned -= OnEnemySpawned;
    }

    private void OnEnemySpawned(GameObject enemy)
    {
      enemy.GetComponent<EnemyDeath>().Happened += OnEnemyDeath;
    }

    private void OnEnemyDeath(EnemyTypeId typeId, GameObject enemy)
    {
      enemy.GetComponent<EnemyDeath>().Happened -= OnEnemyDeath;
      
      playerScore.IncScore(EnemyCost(typeId));
    }

    private int EnemyCost(EnemyTypeId typeId)
    {
      for (int i = 0; i < scoreStaticData.Scores.Length; i++)
      {
        if (scoreStaticData.Scores[i].Type == typeId)
          return scoreStaticData.Scores[i].Cost;
      }
      return 0;
    }
  }
}