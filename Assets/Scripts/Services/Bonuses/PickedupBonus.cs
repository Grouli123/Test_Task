using Bonuses;
using UnityEngine;

namespace Services.Bonuses
{
  public struct PickedupBonus
  {
    public BonusTypeId Id;
    public Bonus Bonus;

    public PickedupBonus(BonusTypeId id, Bonus bonus)
    {
      Id = id;
      Bonus = bonus;
    }
  }
}