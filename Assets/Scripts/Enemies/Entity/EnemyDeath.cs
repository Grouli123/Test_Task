using System;
using Enemies.Spawn;
using UnityEngine;

namespace Enemies.Entity
{
  public class EnemyDeath : MonoBehaviour
  {
    private EnemyTypeId id;
    
    public event Action<EnemyTypeId, GameObject> Happened;
    public event Action Revived;
    
    public void Construct(EnemyTypeId id) => 
      this.id = id;

    public void NotifyAboutDead() => 
      Happened?.Invoke(id, gameObject);

    public void Revive() => 
      Revived?.Invoke();
  }
}