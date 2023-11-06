using Enemies;
using Enemies.Spawn;
using UnityEngine;

namespace StaticData.Enemies
{
  [CreateAssetMenu(fileName = "MonsterStaticData", menuName = "Static Data/Enemies/Create Enemy Data", order = 55)]
  public class EnemyStaticData : ScriptableObject
  {
    public EnemyTypeId Id;
    public float Hp;
    public EnemiesMoveStaticData MoveData;
    public EnemyAttackStaticData AttackData;
    public GameObject Prefab;
  }
}