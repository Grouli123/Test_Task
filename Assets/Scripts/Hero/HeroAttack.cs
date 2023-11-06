using Interfaces;
using Services.PlayerData;
using StaticData.Hero.Components;
using UnityEngine;

namespace Hero
{
  public class HeroAttack : MonoBehaviour
  {
    [SerializeField] private Transform attackPoint;

    private HeroAttackStaticData attackData;
    private PlayerCharacteristics characteristics;

    private Collider[] hits;

    public void Construct(HeroAttackStaticData data, PlayerCharacteristics characteristics)
    {
      attackData = data;
      hits = new Collider[attackData.MaxAttackedEntitiesCount];
      this.characteristics = characteristics;
    }

    public void Attack()
    {
      for (int i = 0; i < Hit(); i++)
      {
        hits[i].GetComponentInChildren<IDamageableEntity>().TakeDamage(characteristics.Damage(), transform.position);
      }
    }

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(attackPoint.position, attackData.AttackRadius, hits, attackData.Mask);
  }
}