using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _quantity;
    [SerializeField] private Button _removeButton;

    private Sprite _baseSprite;

    private Inventory _inventory = null;
    private Item _item;

    private void Start()
    {
        if (_itemImage == null || _quantity == null || _removeButton == null)
            throw new ArgumentNullException();

        _baseSprite = _itemImage.sprite;
    }

    public void InstanceSlot(Inventory inventory)
    {
        if (_inventory != null)
            throw new InvalidOperationException(nameof(_inventory));

        _inventory = inventory;
    }

    public void UpdateSlot(Item itemInSlot, int quantityInSlot)
    {
        _item = itemInSlot;

        if (!_inventory.HasItemInContainer(itemInSlot))
        {
            _removeButton.enabled = false;
            _itemImage.sprite = _baseSprite;
            _quantity.enabled = false;

            return;
        }

        if (itemInSlot != null && quantityInSlot != 0)
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
        if (_inventory != null && _item != null)
        {
            _inventory.RemoveItem(_inventory.ItemList[_inventory.ItemList.IndexOf(_item)], 1);
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
}
