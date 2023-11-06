using System;

namespace Systems.Healths
{
  public interface IHealth : IChangedValue
  {
    void SetHp(float current, float max);
    void TakeDamage(float damage);
    event Action Dead;
    void AddHealth(int value);
  }
}