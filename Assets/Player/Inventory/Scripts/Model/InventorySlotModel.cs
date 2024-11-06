using System;
using UnityEngine;

public class InventorySlotModel : Container
{
    private const int _slotCount = 1;

    public override int SlotCount => _slotCount;

    public event Action<InventorySlotModel> Update;

    public override int AddItem(Item itemAdded, int quantityAdded)
    {
        base.AddItem(itemAdded, quantityAdded);
        Update?.Invoke(this);
        print(MathF.Max(quantityAdded - _quantityList[0], 0));
        return (int)MathF.Max(quantityAdded - _quantityList[0], 0);
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
