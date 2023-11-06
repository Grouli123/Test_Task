using UnityEngine;

namespace StaticData.Hero.Components
{
  [CreateAssetMenu(fileName = "HeroImpactsStaticData", menuName = "Static Data/Hero/Create Hero Impacts Data", order = 55)]
  public class HeroImpactsStaticData : ScriptableObject
  {
    public float ImpactCooldown;
    public float ShieldImpactCooldown;
  }
}