using UnityEngine;

namespace Interfaces
{
  public interface IDamageableEntity
  {
    void TakeDamage(float damage, Vector3 attackPosition);
  }
}
