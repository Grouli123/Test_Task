using System;
using Enemies.Spawn;

namespace StaticData.Level
{
  [Serializable]
  public struct WaveEnemy
  {
    public EnemyTypeId Id;
    public int Count;
    public float HpCoeff;
    public float DamageCoeff;
  }
}