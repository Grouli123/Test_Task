using UnityEngine;

namespace Enemies.Spawn
{
  public class SpawnPoint : MonoBehaviour
  {
    public string Id { get; private set; }

    public void Construct(string id) =>
      Id = id;
  }
}