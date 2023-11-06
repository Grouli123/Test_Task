using System;

namespace Loots
{
  [Serializable]
  public class Characteristic
  {
    public CharacteristicType Type;
    public int Value;

    public void ChangeValue(int updatedValue) => 
      Value += updatedValue;
  }
}