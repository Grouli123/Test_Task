using Loots;
using StaticData.Hero.Components;
using UnityEngine;

namespace StaticData.Hero
{
  [CreateAssetMenu(fileName = "HeroBaseStaticData", menuName = "Static Data/Hero/Create Hero Base Data", order = 55)]
  public class HeroBaseStaticData : ScriptableObject
  {
    public Characteristic[] Characteristics;
    public int InventorySlotCount = 20;
    public LootType[] EquipmentSlots;
    public HeroStaminaStaticData StaminaStaticData;
    public HeroAttackStaticData AttackData;
    public HeroImpactsStaticData ImpactsData;
  }
}