using Services.UI.Factory;
using Services.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Services.UI.Buttons
{
  public class OpenWindowButton : MonoBehaviour
  {
    [SerializeField] private Button button;
    [SerializeField] private WindowId openWindowId;
    protected IWindowsService windowsService;

    public void Construct(IWindowsService windowsService) => 
      this.windowsService = windowsService;

    private void Awake() => 
      button.onClick.AddListener(Open);

    private void OnDestroy() => 
      button.onClick.RemoveListener(Open);

    protected virtual void Open() => 
      windowsService.Open(openWindowId);
  }
}