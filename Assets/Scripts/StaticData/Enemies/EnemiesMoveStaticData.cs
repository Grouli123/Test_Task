using UnityEngine;

namespace StaticData.Enemies
{
  [CreateAssetMenu(fileName = "EnemiesMoveStaticData", menuName = "Static Data/Enemies/Create Enemy Move Data", order = 55)]
  public class EnemiesMoveStaticData : ScriptableObject
  {
    public float RunSpeed;
    public float WalkSpeed;
    public float DistanceToWalk;
    public float DistanceToAttack;
  }
}