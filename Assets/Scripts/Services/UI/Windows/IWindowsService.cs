using Services.UI.Factory;

namespace Services.UI.Windows
{
  public interface IWindowsService : IService
  {
    void Open(WindowId windowId);
    void Close(WindowId windowId);
  }
}