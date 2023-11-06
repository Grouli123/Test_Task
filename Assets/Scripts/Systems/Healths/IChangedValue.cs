using System;

namespace Systems.Healths
{
  public interface IChangedValue
  {
    event Action<float, float> Changed;
  }
}