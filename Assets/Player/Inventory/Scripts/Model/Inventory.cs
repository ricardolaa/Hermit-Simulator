using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SlotsStorage))]
public class Inventory : MonoBehaviour
{
    [SerializeField] private int _slotCount;
    [SerializeField] private SlotsStorage _slotsStorage;

    public SlotsStorage SlotsStorage => _slotsStorage;
    public GameObject InventoryPanel => _slotsStorage.SlotsPanel;

    public int SlotCount => _slotCount;

    private void Awake()
    {
        if (_slotsStorage == null)
            _slotsStorage = GetComponent<SlotsStorage>();

        if (_slotsStorage != null)
            _slotsStorage.CreateSlots(this);
    }

    public void AddItem(Item item, int quantity)
    {
        int numberArrivals = 0;
        InventorySlotModel slot = _slotsStorage.GetFreeSlotsForItem(item);

        if (slot == null)
            return;

        while (numberArrivals < quantity)
        {
            int remainingToAdd = quantity - numberArrivals;
            int noAdded = slot.AddItem(item, remainingToAdd);

            numberArrivals += (remainingToAdd - noAdded);

            slot = _slotsStorage.GetFreeSlotsForItem(item);


        }

    }

}
