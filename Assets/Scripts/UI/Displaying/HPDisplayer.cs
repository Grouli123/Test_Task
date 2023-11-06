using Systems.Healths;
using UnityEngine;

namespace UI.Displaying
{
  public class HPDisplayer : MonoBehaviour
  {
    [SerializeField] private UIBar hpBar;

    private IHealth health;
    
    public void Construct(IHealth health)
    {
      this.health = health;
      this.health.Changed += UpdateHpBar;
    }

    private void OnDestroy()
    {
      if (health != null)
        health.Changed -= UpdateHpBar;
    }

    private void UpdateHpBar(float current, float max) => 
      hpBar.SetValue(current, max);
  }
}