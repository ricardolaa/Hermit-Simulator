using System.Collections.Generic;
using UnityEngine;

public class SlotsStorageUI : MonoBehaviour
{
    [SerializeField] private GameObject _slotsPanel;
    [SerializeField] private GameObject _slotPrefab;

    private List<InventorySlot> _slots = new List<InventorySlot>();

    public GameObject SlotsPanel => _slotsPanel;
    public List<InventorySlot> Slots => _slots;

    public Transform SlotParent => _slotsPanel.transform;

    public void CreateSlots(Inventory inventory)
    {
        var slotCount = inventory.SlotCount;

        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObject = Instantiate(_slotPrefab, SlotParent);
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();

            if (slot == null)
                throw new System.ArgumentNullException(nameof(slot));

            _slots.Add(slot);
            slot.InstanceSlot(inventory);
        }
    }

    public void UpdateAllSlots(Inventory inventory)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            Item itemInSlot = inventory.GetItemAtIndex(i);
            int quantityInSlot = inventory.GetQuantityAtIndex(i);

            _slots[i].UpdateSlot(itemInSlot, quantityInSlot);
        }
    }

}
