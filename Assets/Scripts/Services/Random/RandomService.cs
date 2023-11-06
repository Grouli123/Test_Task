namespace Services.Random
{
  public class RandomService : IRandomService
  {
    private readonly System.Random random;

    public RandomService()
    {
      random = new System.Random();
    }

    public int Next(int min, int max) =>
      random.Next(min, max);

    public int Next(int max) => 
      Next(0, max);

    public float NextFloat() => 
      (float) NextDouble();

    public double NextDouble() => 
      random.NextDouble();
  }
}