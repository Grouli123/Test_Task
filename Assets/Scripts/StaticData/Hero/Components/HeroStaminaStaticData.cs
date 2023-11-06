using UnityEngine;

namespace StaticData.Hero.Components
{
  [CreateAssetMenu(fileName = "HeroStaminaStaticData", menuName = "Static Data/Hero/Create Hero Stamina Data", order = 55)]
  public class HeroStaminaStaticData : ScriptableObject
  {
    public float AttackCost;
    public float RollCost;
    public float RecoveryRate;
    public float RecoveryCount;
  }
}