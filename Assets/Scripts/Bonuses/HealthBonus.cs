using Systems.Healths;
using Hero;
using UnityEngine;

namespace Bonuses
{
  public class HealthBonus : Bonus
  {
    protected override void Pickup(Collider other, int value) => 
      other.GetComponentInChildren<IHealth>().AddHealth(value);

    protected override bool IsCanBePickedUp(Collider other) => 
      other.TryGetComponent(out HeroStateMachine hero);
  }
}