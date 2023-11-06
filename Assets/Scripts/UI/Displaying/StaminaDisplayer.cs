using Systems.Healths;
using UnityEngine;

namespace UI.Displaying
{
  public class StaminaDisplayer : MonoBehaviour
  {
    [SerializeField] private UIBar hpBar;

    private IChangedValue stamina;
    
    public void Construct(IChangedValue stamina)
    {
      this.stamina = stamina;
      this.stamina.Changed += UpdateHpBar;
    }

    private void OnDestroy()
    {
      if (stamina != null)
        stamina.Changed -= UpdateHpBar;
    }

    private void UpdateHpBar(float current, float max) => 
      hpBar.SetValue(current, max);
  }
}