using StaticData.Loot.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Inventories
{
  public class UIInventorySlot : UIBaseBagSlot
  {
    [SerializeField] private TextMeshProUGUI countText;

    public void SetItem(ItemStaticData item, int count)
    {
      SaveItemSlot(item);

      if (item == null)
        SetDefaultView();
      else
      {
        SetItemView(item);
        UpdateCountText(count);
      }
    }

    protected override void SetItemView(ItemStaticData item)
    {
      itemView.sprite = item.Icon;
      ChangeImageEnableState(itemView, true);
    }

    protected override void SetDefaultView()
    {
      ChangeImageEnableState(itemView, false);
      RemoveCountText();
    }

    private void UpdateCountText(int count) => 
      countText.text = count.ToString();

    private void RemoveCountText() => 
      countText.text = "";
  }
}