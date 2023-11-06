using Enemies.Entity;

namespace Services.Loot
{
  public interface ILootService : ICleanupService
  {
    void SetSceneName(string name);
  }
}