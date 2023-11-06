using Services.UI.Factory;
using Services.UI.Windows;

namespace Services.Hero
{
  public class HeroDeathService : IHeroDeathService
  {
    private readonly IWindowsService windowsService;

    public HeroDeathService(IWindowsService windowsService)
    {
      this.windowsService = windowsService;
    }

    public void Dead()
    {
      windowsService.Open(WindowId.DeathMenu);
    }
  }
}