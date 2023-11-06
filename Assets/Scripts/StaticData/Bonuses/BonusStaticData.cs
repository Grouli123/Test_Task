using Bonuses;
using UnityEngine;

namespace StaticData.Bonuses
{
  [CreateAssetMenu(fileName = "BonusStaticData", menuName = "Static Data/Bonus/Create Bonus Data", order = 55)]
  public class BonusStaticData : ScriptableObject
  {
    public BonusTypeId Type;
    public Bonus Prefab;
  }
}