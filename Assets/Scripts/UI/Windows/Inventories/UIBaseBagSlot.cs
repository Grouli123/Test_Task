using System;
using Loots;
using StaticData.Loot.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Inventories
{
  public abstract class UIBaseBagSlot : MonoBehaviour
  {
    [SerializeField] protected Image itemView;
    [SerializeField] private Button clickButton;

    public ItemStaticData EquippedItem { get; protected set; }
    
    public event Action<ItemStaticData> Clicked;

    private void Awake() => 
      clickButton.onClick.AddListener(NotifyAboutClick);

    private void OnDestroy() => 
      clickButton.onClick.RemoveListener(NotifyAboutClick);

    protected abstract void SetItemView(ItemStaticData item);

    protected abstract void SetDefaultView();

    protected void SaveItemSlot(ItemStaticData item) => 
      EquippedItem = item;

    protected void ChangeImageEnableState(Image image, bool isEnable) => 
      image.enabled = isEnable;

    private void NotifyAboutClick() => 
      Clicked?.Invoke(EquippedItem);
  }
}