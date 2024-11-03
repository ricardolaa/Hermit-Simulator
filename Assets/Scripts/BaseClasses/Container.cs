using System.Collections.Generic;
using UnityEngine;

public abstract class Container : MonoBehaviour
{
    [SerializeField] protected int _slotCount;

    protected List<Item> _itemList = new List<Item>();
    protected List<int> _quantityList = new List<int>();

    public List<Item> ItemList => _itemList;
    public List<int> QuantityList => _quantityList;

    public virtual void AddItem(Item itemAdded, int quantityAdded)
    {
        int index = _itemList.IndexOf(itemAdded);

        if (itemAdded.Stacable)
        {
            if (index >= 0)
            {
                _quantityList[index] += quantityAdded;
            }
            else
            {
                if (_itemList.Count < _slotCount)
                {
                    _itemList.Add(itemAdded);
                    _quantityList.Add(quantityAdded);
                }
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

    public virtual void RemoveItem(Item itemRemoved, int quantityRemoved)
    {
        int index = _itemList.IndexOf(itemRemoved);

        if (index >= 0)
        {
            if (itemRemoved.Stacable)
            {
                if (_quantityList[index] >= quantityRemoved)
                {
                    _quantityList[index] -= quantityRemoved;
                    if (_quantityList[index] <= 0)
                    {
                        _quantityList.RemoveAt(index);
                        _itemList.RemoveAt(index);
                    }
                }
            }
            else
            {
                for (int i = 0; i < quantityRemoved; i++)
                {
                    if (_itemList.Count > 0)
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
}
