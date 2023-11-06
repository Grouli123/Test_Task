namespace Services
{
  public interface IService
  {
  }

  public interface ICleanupService : IService
  {
    void Cleanup();
  }
}