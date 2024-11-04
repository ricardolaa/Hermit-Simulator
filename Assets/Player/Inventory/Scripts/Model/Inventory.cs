using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SlotsStorageUI))]
public class Inventory : Container
{
    [SerializeField] private SlotsStorageUI _slotsStorageUI;

    public SlotsStorageUI SlotsStorageUI => _slotsStorageUI;
    public GameObject InventoryPanel => _slotsStorageUI.SlotsPanel;

    private void Awake()
    {
        if (_slotsStorageUI == null)
            _slotsStorageUI = GetComponent<SlotsStorageUI>();

        if (_slotsStorageUI != null)
            _slotsStorageUI.CreateSlots(this);
    }

    public override void AddItem(Item itemAdded, int quantityAdded)
    {
        base.AddItem(itemAdded, quantityAdded);
        _slotsStorageUI.UpdateAllSlots(this);
    }

    public override void RemoveItem(Item itemRemoved, int quantityRemoved)
    {
        base.RemoveItem(itemRemoved, quantityRemoved);
        _slotsStorageUI.UpdateAllSlots(this);
    }
   
}
