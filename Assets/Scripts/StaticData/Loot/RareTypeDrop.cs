using System;
using Loots;

namespace StaticData.Loot
{
  [Serializable]
  public struct RareTypeDrop
  {
    public LootRareType RareType;
    public float Chance;
  }
}