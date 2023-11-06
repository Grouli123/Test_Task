using System;
using Enemies.Spawn;
using UnityEngine;

namespace StaticData.Loot
{
  [Serializable]
  public struct EnemyLoot
  {
    public EnemyTypeId[] TypeIds;
    public RareTypeDrop[] RareTypeDrops;
    public Vector2Int LootCountRange;
  }
}