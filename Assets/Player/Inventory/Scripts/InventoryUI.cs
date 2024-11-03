using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI
{
    private List<InventorySlot> _slotList = new List<InventorySlot>();

    public InventoryUI(GameObject inventoryPanel)
    {
        if (inventoryPanel == null)
            throw new System.ArgumentNullException(nameof(inventoryPanel));

        _slotList = inventoryPanel.GetComponentsInChildren<InventorySlot>().ToList();
    }

    public void UpdateInventoryUI(List<Item> itemList, List<int> quantityList)
    {
        int ind = 0;

        foreach (InventorySlot slot in _slotList)
        {

            if (itemList.Count != 0)
            {

                if (ind < itemList.Count)
                {
                    slot.UpdateSlot(itemList[ind], quantityList[ind]);
                    ind = ind + 1;
                }
                else
                {
                    slot.UpdateSlot(null, 0);
                }
            }
            else
            {
                slot.UpdateSlot(null, 0);
            }

        }
    }
}
