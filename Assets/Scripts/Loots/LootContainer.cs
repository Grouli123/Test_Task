using System.Linq;
using StaticData.Loot.Items;
using UnityEngine;

namespace Loots
{
    public class LootContainer : MonoBehaviour
    {
        [SerializeField] private ItemStaticData[] itemsData;

        public ItemStaticData[] RareTypeItems(LootRareType rareType)
        {
            ItemStaticData[] items = itemsData.Where(element => element.Rarity == rareType).ToArray();
            return items;
        }
    }
}
