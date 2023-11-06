using StaticData.Level;

namespace Services.Waves
{
  public interface IWaveServices : ICleanupService
  {
    void Start();
    void SetLevelWaves(LevelWaveStaticData wavesData);
  }
}