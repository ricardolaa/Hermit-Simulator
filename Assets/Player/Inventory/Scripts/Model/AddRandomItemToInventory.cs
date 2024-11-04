using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomItemToInventory : MonoBehaviour
{
  
    [SerializeField] private List<Item> _itemsToGive = new List<Item>();

    [SerializeField] private int _minimumItemsToGive = 1;

    [SerializeField] private int _maximumItemsToGive = 1;

    [SerializeField] private Inventory _inventory;

    public void AddRandomItem()
    {
        _inventory.AddItem(_itemsToGive[Random.Range(0, _itemsToGive.Count)], Random.Range(_minimumItemsToGive, _maximumItemsToGive));
    }

}
