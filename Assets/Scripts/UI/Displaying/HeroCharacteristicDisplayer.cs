using Services.PlayerData;
using TMPro;
using UnityEngine;

namespace UI.Windows.Inventories
{
  public class HeroCharacteristicDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI healthValueText;
    [SerializeField] private TextMeshProUGUI strengthValueText;
    [SerializeField] private TextMeshProUGUI staminaValueText;
    
    private PlayerCharacteristics playerCharacteristics;

    public void Construct(PlayerCharacteristics playerCharacteristics)
    {
      this.playerCharacteristics = playerCharacteristics;
      this.playerCharacteristics.Changed += DisplayCharacteristics;
      DisplayCharacteristics();
    }

    public void Cleanup() => 
      playerCharacteristics.Changed -= DisplayCharacteristics;

    private void DisplayCharacteristics()
    {
      healthValueText.text = playerCharacteristics.Health().ToString();
      strengthValueText.text = playerCharacteristics.Damage().ToString();
      staminaValueText.text = playerCharacteristics.Stamina().ToString();
    }
  }
}