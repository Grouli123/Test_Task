using UnityEngine;

namespace Bonuses
{
  public abstract class BonusUseStrategy : MonoBehaviour
  {
    public abstract void Pickup(Collider other, int value);

    public abstract bool IsCanBePickedUp(Collider other);
  }
}