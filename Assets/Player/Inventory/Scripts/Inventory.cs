using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Container
{
    [SerializeField] private GameObject _inventoryPanel;

    public InventoryUI inventoryUI { get; private set; }

    private void Awake()
    {
        var _slotList = _inventoryPanel.GetComponentsInChildren<InventorySlot>().ToList();

        foreach (InventorySlot slot in _slotList)
        {
            slot.InstanceInventory(this);
        }
    }

    private void Start()
    {
        inventoryUI = new InventoryUI(_inventoryPanel);
    }

    public override void AddItem(Item itemAdded, int quantityAdded)
    {
        base.AddItem(itemAdded, quantityAdded);
        inventoryUI.UpdateInventoryUI(ItemList, QuantityList);
    }

    public override void RemoveItem(Item itemRemoved, int quantityRemoved)
    {
        base.RemoveItem(itemRemoved, quantityRemoved);
        inventoryUI.UpdateInventoryUI(ItemList, QuantityList);
    }
   
}
