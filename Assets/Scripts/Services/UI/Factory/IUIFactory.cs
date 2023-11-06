using System;
using UI.Base;

namespace Services.UI.Factory
{
  public interface IUIFactory : IService
  {
    event Action<WindowId,BaseWindow> Spawned;
    void CreateWindow(WindowId id);
    void CreateUIRoot();
  }
}