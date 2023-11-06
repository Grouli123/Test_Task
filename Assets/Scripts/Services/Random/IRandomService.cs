namespace Services.Random
{
  public interface IRandomService : IService
  {
    int Next(int min, int max);
    int Next(int max);
    float NextFloat();
    double NextDouble();
  }
}