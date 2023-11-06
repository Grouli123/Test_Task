using Enemies.Spawn;
using StaticData.Level;

namespace Services.Bonuses
{
  public interface IBonusSpawner : ICleanupService
  {
    void AddPoint(SpawnPoint spawnPoint);
    void SpawnBonus(WaveBonus bonus);
  }
}