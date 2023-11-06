using System;
using Bonuses;

namespace StaticData.Level
{
  [Serializable]
  public struct WaveBonus
  {
    public BonusTypeId TypeId;
    public int Value;
    public int Count;
  }
}