using System.Collections.Generic;
using System.Linq;
using Services.UI.Buttons;
using Services.UI.Factory;
using UI.Base;

namespace Services.UI.Windows
{
  public class WindowsService : IWindowsService
  {
    private readonly IUIFactory uiFactory;

    private readonly Dictionary<WindowId, BaseWindow> windows;

    public WindowsService(IUIFactory uiFactory)
    {
      this.uiFactory = uiFactory;
      this.uiFactory.Spawned += InitSpawnedWindowAndSave;
      windows = new Dictionary<WindowId, BaseWindow>(10);
    }

    public void Open(WindowId windowId)
    {
      switch (windowId)
      {
        case WindowId.None:
          break;
        default:
          OpenWindow(windowId);
          break;
      }
    }

    public void Close(WindowId windowId)
    {
      if (windows.ContainsKey(windowId))
        windows[windowId].Close();
    }

    private void OpenWindow( WindowId windowId)
    {
      if (windows.ContainsKey(windowId) == false)
        uiFactory.CreateWindow(windowId);
      
      windows[windowId].Open();
    }

    private void InitSpawnedWindowAndSave(WindowId windowId, BaseWindow window)
    {
      InitButtons(window);

      if (windows.ContainsKey(windowId) == false)
        AddSpawnedWindow(windowId, window);
    }

    private void InitButtons(BaseWindow window)
    {
      OpenWindowButton[] buttons = window.GetComponentsInChildren<OpenWindowButton>(true);
      for (int i = 0; i < buttons.Length; i++)
      {
        buttons[i].Construct(this);
      }
    }

    private void AddSpawnedWindow(WindowId windowId, BaseWindow window)
    {
      windows.Add(windowId, window);
      window.Destroyed += RemoveWindow;
    }

    private void RemoveWindow(BaseWindow destroyedWindow)
    {
      WindowId key = windows.FirstOrDefault(x => x.Value == destroyedWindow).Key;
      if (key != WindowId.None)
      {
        windows[key].Destroyed -= RemoveWindow;
        windows.Remove(key);
      }
    }
  }
}