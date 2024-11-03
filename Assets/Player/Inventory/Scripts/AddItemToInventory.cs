using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemToInventory : MonoBehaviour
{
    [SerializeField] private Item _specificItem;

    [SerializeField] private int _specificQuant;

    [SerializeField] private Inventory _inventory;

    public void AddSpecificItem()
    {
        _inventory.AddItem(_specificItem, _specificQuant);
    }
 
}
