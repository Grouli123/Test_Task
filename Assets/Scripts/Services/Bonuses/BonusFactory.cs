using System.Collections.Generic;
using Bonuses;
using Services.Assets;
using Services.StaticData;
using StaticData.Bonuses;
using UnityEngine;

namespace Services.Bonuses
{
  public class BonusFactory : IBonusFactory
  {
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;

    private readonly Dictionary<BonusTypeId,Queue<PickedupBonus>> bonusesPool;
    
   
    public BonusFactory(IAssetProvider assets, IStaticDataService staticData)
    {
      this.assets = assets;
      this.staticData = staticData;
      bonusesPool = new Dictionary<BonusTypeId,Queue<PickedupBonus>>(10);
    }

    public void Cleanup()
    {
      PickedupBonus bonus;
      foreach (KeyValuePair<BonusTypeId,Queue<PickedupBonus>> bonuses in bonusesPool)
      {
        while (bonuses.Value.Count > 0)
        {
          bonus = bonuses.Value.Dequeue();
          if (bonus.Bonus != null)
            bonus.Bonus.PickedUp -= OnBonusPickedup;
        }
      }

      bonusesPool.Clear();
    }
    
    public Bonus SpawnBonus(BonusTypeId typeId, int value, Transform parent)
    {
      if (IsContainsInPool(typeId))
        return RecreateBonus(typeId, value, parent);

      return CreateBonus(typeId, value, parent);
    }

    private Bonus RecreateBonus(BonusTypeId typeId, int value, Transform parent)
    {
      PickedupBonus pickedupBonus = PooledBonus(typeId);
      
      Bonus bonus = pickedupBonus.Bonus;
      
      bonus.SetValue(value);
      
      bonus.SetPosition(parent.position);
      
      return bonus;
    }

    private Bonus CreateBonus(BonusTypeId typeId, int value, Transform parent)
    {
      BonusStaticData bonusData = staticData.ForBonus(typeId);
      Bonus bonus = assets.Instantiate(bonusData.Prefab, parent.position, Quaternion.identity, parent);
      bonus.PickedUp += OnBonusPickedup;
      bonus.SetValue(value);
      return bonus;
    }

    private void OnBonusPickedup(Bonus bonus)
    {
      if (bonusesPool.ContainsKey(bonus.Type))
        bonusesPool[bonus.Type].Enqueue(new PickedupBonus(bonus.Type, bonus));
      else
        bonusesPool.Add(bonus.Type, new Queue<PickedupBonus>(new[]{new PickedupBonus(bonus.Type, bonus) }));
      
      bonus.Hide();
    }

    private bool IsContainsInPool(BonusTypeId typeId) => 
      bonusesPool.ContainsKey(typeId) && bonusesPool[typeId].Count > 0;

    private PickedupBonus PooledBonus(BonusTypeId typeId) => 
      bonusesPool[typeId].Dequeue();
  }
}