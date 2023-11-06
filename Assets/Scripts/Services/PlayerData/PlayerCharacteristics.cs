using System;
using System.Collections.Generic;
using Loots;

namespace Services.PlayerData
{
  public class PlayerCharacteristics
  {
    private Dictionary<CharacteristicType,Characteristic> characteristics = new Dictionary<CharacteristicType, Characteristic>(20);

    public event Action Changed;

    public PlayerCharacteristics(Characteristic[] characteristics) => 
      SetDefaultValue(characteristics);

    public void SetDefaultValue(Characteristic[] heroCharacteristics)
    {
      characteristics.Clear();
      for (int i = 0; i < heroCharacteristics.Length; i++)
      {
        characteristics.Add(heroCharacteristics[i].Type, heroCharacteristics[i]);
      }
    }

    public int Stamina() => 
      characteristics[CharacteristicType.Stamina].Value;

    public int Damage() => 
      characteristics[CharacteristicType.Strength].Value;

    public int Health() => 
      characteristics[CharacteristicType.Health].Value;

    public void IncCharacteristic(CharacteristicType type, int value)
    {
      if (characteristics.ContainsKey(type))
      {
        characteristics[type].ChangeValue(value);
        NotifyAboutChange();
      }
    }

    public void ReduceCharacteristic(CharacteristicType type, int value)
    {
      if (characteristics.ContainsKey(type))
      {
        characteristics[type].ChangeValue(-value);
        NotifyAboutChange();
      }
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke();
  }
}