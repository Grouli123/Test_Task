using System;
using System.Collections;
using Systems.Healths;
using Services.PlayerData;
using StaticData.Hero.Components;
using UnityEngine;

namespace Hero
{
  public class HeroStamina : MonoBehaviour, IStamina
  {
    private float currentStamina;
    private Coroutine recoveryCoroutine;
    
    private HeroStaminaStaticData staminaData;
    private PlayerCharacteristics characteristics;

    public event Action<float, float> Changed;

    public void Construct(HeroStaminaStaticData staminaData, PlayerCharacteristics characteristics)
    {
      this.staminaData = staminaData;
      this.characteristics = characteristics;
      SetDefaultValue();
    }
    
    public bool IsCanAttack() => 
      currentStamina - staminaData.AttackCost >= 0;

    public bool IsCanRoll() => 
      currentStamina - staminaData.RollCost >= 0;

    public void WasteToAttack()
    {
      currentStamina -= staminaData.AttackCost;
      if (recoveryCoroutine == null)
        recoveryCoroutine = StartCoroutine(RecoveryValue());
      Display();
    }

    public void WasteToRoll()
    {
      currentStamina -= staminaData.RollCost;
      if (recoveryCoroutine == null)
        recoveryCoroutine = StartCoroutine(RecoveryValue());
      Display();
    }

    private void SetDefaultValue() => 
      currentStamina = characteristics.Stamina();

    private void Display() => 
      Changed?.Invoke(currentStamina, characteristics.Stamina());

    private IEnumerator RecoveryValue()
    {
      while (currentStamina < characteristics.Stamina())
      {
        yield return new WaitForSeconds(staminaData.RecoveryRate);
        currentStamina += staminaData.RecoveryCount;
        Display();
      }

      recoveryCoroutine = null;
    }
  }
}