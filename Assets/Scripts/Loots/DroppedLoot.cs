using System;
using Hero;
using Interfaces;
using StaticData.Loot.Items;
using UnityEngine;

namespace Loots
{
  [RequireComponent(typeof(BoxCollider))]
  public class DroppedLoot : MonoBehaviour, IPickedupObject<DroppedLoot>
  {
    private ItemStaticData currentItem;

    private GameObject spawnedView;

    public event Action<DroppedLoot> PickedUp;

    public void Show() => 
      gameObject.SetActive(true);

    public void Hide() => 
      gameObject.SetActive(false);

    public void SetPosition(Vector3 position) => 
      transform.position = position;

    public void SetItem(ItemStaticData itemData)
    {
      currentItem = itemData;
      SpawnView(itemData.Prefab);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out HeroInventory inventory))
      {
        if (inventory.IsCanAddItem(currentItem))
          PickUp(inventory);
      }
    }

    private void SpawnView(GameObject prefab)
    {
      if (spawnedView != null)
        Destroy(spawnedView);

      spawnedView = Instantiate(prefab, transform);
    }

    private void PickUp(HeroInventory heroInventory)
    {
      heroInventory.AddItem(currentItem);
      PickedUp?.Invoke(this);
    }
  }
}