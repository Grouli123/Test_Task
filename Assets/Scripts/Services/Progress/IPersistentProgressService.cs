using Services.PlayerData;

namespace Services.Progress
{
  public interface IPersistentProgressService : IService
  {
    Player Player { get; }
    void SetPlayerToDefault();
  }
}