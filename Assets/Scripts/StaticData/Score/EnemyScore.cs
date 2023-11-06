using System;
using Enemies.Spawn;

namespace StaticData.Score
{
  [Serializable]
  public struct EnemyScore
  {
    public EnemyTypeId Type;
    public int Cost;
  }
}