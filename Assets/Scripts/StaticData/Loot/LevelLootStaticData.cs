using UnityEngine;

namespace StaticData.Loot
{
  [CreateAssetMenu(fileName = "LevelLootStaticData", menuName = "Static Data/Loot/Create Loot Data", order = 55)]
  public class LevelLootStaticData : ScriptableObject
  {
    public string LevelKey;
    public EnemyLoot[] Loots;
  }
}