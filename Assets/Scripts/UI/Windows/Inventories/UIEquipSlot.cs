using System;
using Loots;
using StaticData.Loot.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Inventories
{
  public class UIEquipSlot : UIBaseBagSlot
  {
    [SerializeField] private Image defaultView;
    public LootType Type { get; private set; }

    public void SetEquipSlot(Sprite defaultIcon, LootType type)
    {
      defaultView.sprite = defaultIcon;
      Type = type;
    }
    
    public void SetItem(ItemStaticData item)
    {
      if (EquippedItem == item)
        return;
      
      SaveItemSlot(item);
      
      if (item == null)
        SetDefaultView();
      else
        SetItemView(item);
    }
    
    
    public void RemoveItem()
    {
      SaveItemSlot(null);
      SetDefaultView();
    }

    protected override void SetItemView(ItemStaticData item)
    {
      ChangeImageEnableState(defaultView, false);
      ChangeImageEnableState(itemView, true);
      itemView.sprite = item.Icon;
    }

    protected override void SetDefaultView()
    {
      ChangeImageEnableState(defaultView, true);
      ChangeImageEnableState(itemView, false);
    }
  }
}