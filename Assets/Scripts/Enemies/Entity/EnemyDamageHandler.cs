using Systems.Healths;
using Interfaces;
using UnityEngine;

namespace Enemies.Entity
{
  public class EnemyDamageHandler : MonoBehaviour, IDamageableEntity
  {
    [SerializeField] private EnemyStateMachine enemy;
    [SerializeField] private Health health;


    public void TakeDamage(float damage, Vector3 attackPosition)
    {
      health.TakeDamage(damage);
      enemy.Impact();
    }
  }
}