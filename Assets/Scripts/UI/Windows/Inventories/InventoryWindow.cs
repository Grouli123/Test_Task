using Services.PlayerData;
using StaticData.Loot.Items;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Inventories
{
  public class InventoryWindow : BaseWindow
  {
    [SerializeField] private UIEquipArea equipArea;
    [SerializeField] private UIInventoryArea inventoryArea;
    [SerializeField] private InventoryItemTip tip;
    [SerializeField] private HeroCharacteristicDisplayer characteristicDisplayer;
    [SerializeField] private Button useButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button closeButton;
    
    private Equipment equipment;
    private Inventory inventory;
    
    public void Construct(Player player)
    {
      equipment = player.Equipment;
      inventory = player.Inventory;
      equipment.Changed += OnEquipmentChange;
      inventory.Changed += OnInventoryChange;
      equipArea.InitSlots(equipment.Slots);
      characteristicDisplayer.Construct(player.Characteristics);
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      equipArea.EquipClicked += OnEquipClick;
      inventoryArea.InventoryClicked += OnInventoryClicked;
      useButton.onClick.AddListener(UseInventoryItem);
      removeButton.onClick.AddListener(RemoveEquip);
      closeButton.onClick.AddListener(Close);
      inventoryArea.Subscribe();
      
    }

    public override void Open()
    {
      UpdateEquipArea();
      UpdateInventoryArea();
      base.Open();
    }

    public override void Close() => 
      Destroy(gameObject);

    protected override void Cleanup()
    {
      base.Cleanup();
      equipArea.Cleanup();
      inventoryArea.Cleanup();
      characteristicDisplayer.Cleanup();
      equipArea.EquipClicked -= OnEquipClick;
      inventoryArea.InventoryClicked -= OnInventoryClicked;
      equipment.Changed -= OnEquipmentChange;
      inventory.Changed -= OnInventoryChange;
      useButton.onClick.RemoveListener(UseInventoryItem);
      removeButton.onClick.RemoveListener(RemoveEquip);
      closeButton.onClick.RemoveListener(Close);
    }

    private void OnEquipClick(ItemStaticData item)
    {
      inventoryArea.RemoveLastClickedItem();
      
      if (inventory.IsCanAddItem(item))
      {
        ChangeButtonActiveState(removeButton, true);
        ChangeButtonActiveState(useButton, false);
      }
      DisableTip();
      DisplayItemTip(item);
    }

    private void OnInventoryClicked(ItemStaticData item)
    {
      equipArea.RemoveLastClickedItem();
      
      if (equipment.IsCanEquipItem(item))
      {
        ChangeButtonActiveState(useButton, true);
        ChangeButtonActiveState(removeButton, false);
      }
      DisableTip();
      DisplayItemTip(item);
    }

    private void RemoveEquip()
    {
      equipment.RemoveItem(equipArea.LastClickedItem);
      inventory.AddItem(equipArea.LastClickedItem);
      equipArea.RemoveLastClickedItem();
      DisableButtons();
      DisableTip();
    }

    private void UseInventoryItem()
    {
      inventory.RemoveItem(inventoryArea.LastClickedItem);
      equipment.EquipItem(inventoryArea.LastClickedItem);
      inventoryArea.RemoveLastClickedItem();
      DisableButtons();
      DisableTip();
    }

    private void OnEquipmentChange()
    {
      UpdateEquipArea();
      DisableButtons();
    }

    private void OnInventoryChange()
    {
      UpdateInventoryArea();
      DisableButtons();
    }

    private void DisplayItemTip(ItemStaticData item) => 
      tip.DisplayTip(item.Name, item.Characteristics);

    private void DisableTip() => 
      tip.RemoveTip();

    private void DisableButtons()
    {
      ChangeButtonActiveState(removeButton, false);
      ChangeButtonActiveState(useButton, false);
    }

    private void ChangeButtonActiveState(Button button, bool isActive)
    {
      if (button.gameObject.activeSelf != isActive)
        button.gameObject.SetActive(isActive);
    }

    private void UpdateEquipArea() => 
      equipArea.UpdateView(equipment.Slots);

    private void UpdateInventoryArea() => 
      inventoryArea.UpdateView(inventory.Slots);
  }
}