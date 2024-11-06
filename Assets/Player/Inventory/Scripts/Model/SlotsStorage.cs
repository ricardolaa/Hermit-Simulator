using System.Collections.Generic;
using UnityEngine;

public class SlotsStorage : MonoBehaviour
{
    [SerializeField] private GameObject _slotsPanel;
    [SerializeField] private GameObject _slotPrefab;

    private List<InventorySlotModel> _slots = new List<InventorySlotModel>();
    private List<InventorySlotView> _slotsView = new List<InventorySlotView>();

    private Dictionary<InventorySlotModel, (Item, int)> _slotsData = new Dictionary<InventorySlotModel, (Item, int)>();

    public GameObject SlotsPanel => _slotsPanel;
    public List<InventorySlotModel> Slots => _slots;
    public Transform SlotParent => _slotsPanel.transform;
    public Dictionary<InventorySlotModel, (Item, int)> SlotsData => _slotsData;

    public void CreateSlots(Inventory inventory)
    {
        var slotCount = inventory.SlotCount;

        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObject = Instantiate(_slotPrefab, SlotParent);
            InventorySlotModel slot = slotObject.GetComponent<InventorySlotModel>();

            if (slot == null)
                throw new System.ArgumentNullException(nameof(slot));


            slot.Update += UpdateSlot;

            _slots.Add(slot);
            _slotsView.Add(slotObject.GetComponent<InventorySlotView>());
            _slotsData[slot] = (null, 0);
        }
    }

    public void UpdateSlot(InventorySlotModel slot)
    {
        Item itemInSlot = slot.GetItemAtIndex(0);
        int quantityInSlot = slot.GetQuantityAtIndex(0);

        slot.gameObject.GetComponent<InventorySlotView>().UpdateSlot(itemInSlot, quantityInSlot);
        _slotsData[slot] = (itemInSlot, quantityInSlot);
      
    }

    public InventorySlotModel GetFreeSlotsForItem(Item item)
    {
        foreach (var slot in _slots)
        {
            if (_slotsData[slot].Item1 == null)
            {
                return slot;
            }

            if (_slotsData[slot].Item1 == item && _slotsData[slot].Item2 < item.MaxInStack)
            {
                return slot;
            }
        }

        return null;
    }


}
