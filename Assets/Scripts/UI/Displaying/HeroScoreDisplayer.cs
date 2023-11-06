using Services.PlayerData;
using TMPro;
using UnityEngine;

namespace UI.Displaying
{
  public class HeroScoreDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI scoreCountText;
    
    private PlayerScore score;

    public void Construct(PlayerScore score)
    {
      this.score = score;
      this.score.Changed += DisplayScoreCount;
      DisplayScoreCount();
    }

    public void Cleanup() => 
      score.Changed -= DisplayScoreCount;

    private void DisplayScoreCount() => 
      scoreCountText.text = score.Score.ToString();
  }
}