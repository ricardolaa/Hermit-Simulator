using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _quantity;
    [SerializeField] private Button _removeButton;

    private Sprite _baseSprite;

    private void Awake()
    {
        _baseSprite = _itemImage.sprite;
    }

    public void UpdateSlot(Item itemInSlot, int quantityInSlot)
    {
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
}