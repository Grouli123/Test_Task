using System;
using UnityEngine;

namespace Systems.Healths
{
  public class Health : MonoBehaviour, IHealth
  {
    private float maxHealth;
    private float currentHealth;

    public event Action<float, float> Changed;
    public event Action Dead;

    public void SetHp(float current, float max)
    {
      currentHealth = current;
      maxHealth = max;
      Display();
    }

    public void TakeDamage(float damage)
    {
      currentHealth -= damage;
      Display();
      if (currentHealth <= 0)
        Dead?.Invoke();
    }

    public void AddHealth(int value)
    {
      if (currentHealth <= 0)
        return;
      currentHealth += value;
      Display();
    }

    private void Display() => 
      Changed?.Invoke(currentHealth, maxHealth);
  }
}
