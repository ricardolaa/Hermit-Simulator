using System;
using UnityEngine;

public class InventorySlotModel : Container
{
    private const int _slotCount = 1;

    public override int SlotCount => _slotCount;

    public event Action<InventorySlotModel> Update;

    public override int AddItem(Item itemAdded, int quantityAdded)
    {
        int nonArrivals = 0;
        print(quantityAdded);

        if (_quantityList.Count > 0)
        {
            nonArrivals = (int)MathF.Max(quantityAdded - (itemAdded.MaxInStack - _quantityList[0]), 0);
        }
        else
        {
            nonArrivals = (int)MathF.Max(quantityAdded - itemAdded.MaxInStack, 0);
        }

        base.AddItem(itemAdded, quantityAdded);
        Update?.Invoke(this);
        return nonArrivals;
    }

    public override void RemoveItem(Item itemRemoved, int quantityRemoved)
    {
        base.RemoveItem(itemRemoved, quantityRemoved);
        Update?.Invoke(this);
    }

    public void RemoveItem()
    {
        RemoveItem(_itemList[0], 1);
    }

}
