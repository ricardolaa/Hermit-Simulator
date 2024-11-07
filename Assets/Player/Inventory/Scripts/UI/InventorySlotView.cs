using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _quantity;
    [SerializeField] private Button _removeButton;

    private Sprite _baseSprite;
    private Item _item;

    private void Awake()
    {
        _baseSprite = _itemImage.sprite;
    }

    public void UpdateSlot(Item itemInSlot, int quantityInSlot)
    {
        _item = itemInSlot;

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
        else
        {
            _removeButton.enabled = false;
            _itemImage.sprite = _baseSprite;
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
}