using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Container : MonoBehaviour
{
    protected List<Item> _itemList = new List<Item>();
    protected List<int> _quantityList = new List<int>();

    public virtual List<Item> ItemList => _itemList;
    public virtual List<int> QuantityList => _quantityList;

    public abstract int SlotCount { get; }

    public virtual int AddItem(Item itemAdded, int quantityAdded)
    {
        if (quantityAdded < 0)
            throw new ArgumentOutOfRangeException(nameof(quantityAdded));

        if (itemAdded.Stacable)
        {
            HandleStackableItem(itemAdded, ref quantityAdded);
        }
        else
        {
            HandleNonStackableItem(itemAdded, quantityAdded);
        }

        return 0;
    }

    protected virtual void HandleStackableItem(Item itemAdded, ref int quantityAdded)
    {
        int index = _itemList.LastIndexOf(itemAdded);

        if (index >= 0)
        {
            int currentQuantity = _quantityList[index];
            int availableSpace = itemAdded.MaxInStack - currentQuantity;

            if (availableSpace > 0)
            {
                int quantityToAdd = Mathf.Min(quantityAdded, availableSpace);
                _quantityList[index] += quantityToAdd;
                quantityAdded -= quantityToAdd;
            }

            while (quantityAdded > 0 && _itemList.Count < SlotCount)
            {
                _itemList.Add(itemAdded);
                int quantityToAdd = Mathf.Min(quantityAdded, itemAdded.MaxInStack);
                _quantityList.Add(quantityToAdd);
                quantityAdded -= quantityToAdd;
            }
        }
        else
        {
            if (_itemList.Count < SlotCount)
            {
                _itemList.Add(itemAdded);
                _quantityList.Add(quantityAdded);
            }
        }
    }

    protected virtual void HandleNonStackableItem(Item itemAdded, int quantityAdded)
    {
        for (int i = 0; i < quantityAdded; i++)
        {
            if (_itemList.Count < SlotCount)
            {
                _itemList.Add(itemAdded);
                _quantityList.Add(1);
            }
            else
            {
                break;
            }
        }
    }


    public virtual void RemoveItem(Item itemRemoved, int quantityRemoved)
    {
        if (quantityRemoved < 0)
            throw new ArgumentOutOfRangeException(nameof(quantityRemoved));

        int index = _itemList.LastIndexOf(itemRemoved);

        if (index >= 0)
        {
            if (itemRemoved.Stacable)
            {
                _quantityList[index] -= quantityRemoved;

                if (_quantityList[index] <= 0)
                {
                    _quantityList.RemoveAt(index);
                    _itemList.RemoveAt(index);
                }
            }
            else
            {
                for (int i = 0; i < quantityRemoved; i++)
                {
                    if (_itemList.Count > index)
                    {
                        _quantityList.RemoveAt(index);
                        _itemList.RemoveAt(index);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    public virtual Item GetItemAtIndex(int index)
    {
        if (index >= 0 && index < _itemList.Count)
        {
            return _itemList[index];
        }

        return null;
    }

    public virtual int GetQuantityAtIndex(int index)
    {
        if (index >= 0 && index < _quantityList.Count)
        {
            return _quantityList[index];
        }

        return 0;
    }

    public virtual bool HasItemInContainer(Item item)
    {
        return item != null && _itemList.Contains(item);
    }

}
