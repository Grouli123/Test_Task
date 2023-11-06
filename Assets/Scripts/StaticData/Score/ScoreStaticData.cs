using UnityEngine;

namespace StaticData.Score
{
  [CreateAssetMenu(fileName = "ScoreStaticData", menuName = "Static Data/Score/Create Score Data", order = 55)]
  public class ScoreStaticData : ScriptableObject
  {
    public EnemyScore[] Scores;
  }
}