using System.Collections.Generic;
using UnityEngine;

public abstract class Container : MonoBehaviour
{
    [SerializeField] int _slotCount;

    protected List<Item> _itemList = new List<Item>();
    protected List<int> _quantityList = new List<int>();

    public virtual List<Item> ItemList => _itemList;
    public virtual List<int> QuantityList => _quantityList;
    public virtual int SlotCount => _slotCount;

    public virtual void AddItem(Item itemAdded, int quantityAdded)
    {
        int index = _itemList.LastIndexOf(itemAdded);

        if (itemAdded.Stacable)
        {
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

                while (quantityAdded > 0 && _itemList.Count < _slotCount)
                {
                    _itemList.Add(itemAdded);
                    int quantityToAdd = Mathf.Min(quantityAdded, itemAdded.MaxInStack);
                    _quantityList.Add(quantityToAdd);
                    quantityAdded -= quantityToAdd;
                }
            }
            else
            {
                for (int i = 0; i < quantityAdded; i++)
                {
                    if (_itemList.Count < _slotCount)
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
        }
    }

    public virtual void RemoveItem(Item itemRemoved, int quantityRemoved)
    {
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
