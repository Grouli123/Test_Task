using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Entity
{
  public class EnemyMove : MonoBehaviour
  {
    [SerializeField] private NavMeshAgent agent;

    private Transform currentTarget;
    public Vector3 TargetPosition => currentTarget.position;

    private void Update()
    {
      if (currentTarget == null || agent.isStopped)
        return;
      
      agent.SetDestination(currentTarget.position);
    }

    public void ResetTarget() => 
      currentTarget = null;

    public void SetTarget(Transform target) => 
      currentTarget = target;

    public void StartMove() => 
      agent.isStopped = false;

    public void Stop() => 
      agent.isStopped = true;

    public void UpdateSpeed(float speed) => 
      agent.speed = speed;
  }
}