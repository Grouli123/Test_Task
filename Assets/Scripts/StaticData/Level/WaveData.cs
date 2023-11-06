using System;

namespace StaticData.Level
{
  [Serializable]
  public struct WaveData
  {
    public int WaveIndex;
    public WaveEnemy[] Enemies;
    public WaveBonus[] Bonuses;
    public float WaveWaitTime;
  }
}