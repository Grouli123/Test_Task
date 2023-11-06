using UnityEngine;

namespace StaticData.Hero.Components
{
  [CreateAssetMenu(fileName = "Hero Move Static Data", menuName = "Static Data/Hero/Create Hero Move Data", order = 55)]
  public class HeroMoveStaticData : ScriptableObject
  {
    public float MoveSpeed = 3f;
    public float StrafeSpeed = 1.5f;
    public float RollSpeed = 2f;
  }
}