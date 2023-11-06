using UnityEngine;
using UnityEngine.UI;

namespace UI.Displaying
{
  public class UIBar : MonoBehaviour
  {
    [SerializeField] private Image fillBar;
    public void SetValue(float current, float max) => 
      fillBar.fillAmount = current / max;
  }
}