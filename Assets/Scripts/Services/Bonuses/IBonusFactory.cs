using Bonuses;
using UnityEngine;

namespace Services.Bonuses
{
  public interface IBonusFactory : ICleanupService
  {
    Bonus SpawnBonus(BonusTypeId typeId, int value, Transform parent);
  }
}