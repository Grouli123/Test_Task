using StaticData.Hero.Components;
using UnityEngine;

namespace StaticData.Hero
{
  [CreateAssetMenu(fileName = "SpawnData", menuName = "Static Data/Hero/Create Hero Instantiate Data", order = 55)]
  public class HeroSpawnStaticData : ScriptableObject
  {
    public GameObject HeroPrefab;
    public Vector3 SpawnPoint;
  }
}