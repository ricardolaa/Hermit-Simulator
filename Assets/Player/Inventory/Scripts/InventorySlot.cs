using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _quantity;
    [SerializeField] private Button _removeButton;

    private Inventory _inventory = null; 
    private Item _item;

    public void InstanceInventory(Inventory inventory)
    {
        if (_inventory != null)
            throw new InvalidOperationException(nameof(_inventory));

        _inventory = inventory;
    }

    public void UpdateSlot(Item itemInSlot, int quantityInSlot)
    {
        _item = itemInSlot;

        if (itemInSlot != null && quantityInSlot !=0)
        {

            _removeButton.enabled = true;
            _itemImage.enabled = true; 
            
            _itemImage.sprite = itemInSlot.ItemIcon;

            if (quantityInSlot > 1)
            {
               
                _quantity.enabled = true;
                _quantity.text = quantityInSlot.ToString();
            }
            else
            {
                _quantity.enabled = false;
                
            }

        }
        else
        {
            
            _removeButton.enabled = false;
            _itemImage.enabled = false;
            _quantity.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<ItemInfoUpdate>().UpdateInfoPanel(_item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInParent<ItemInfoUpdate>().ClosePanel();
    }

    public void UseItem()
    {
        if (_item != null)
        {   
            _item.Use();
        }
    }

    public void RemoveItem()
    {
        _inventory.RemoveItem(_inventory.ItemList[_inventory.ItemList.IndexOf(_item)], 1);
    }
}
