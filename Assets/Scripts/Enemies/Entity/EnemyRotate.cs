using UnityEngine;

namespace Enemies.Entity
{
  public class EnemyRotate : MonoBehaviour
  {
    public void LookAt(Vector3 position) => 
      transform.LookAt(position);
  }
}