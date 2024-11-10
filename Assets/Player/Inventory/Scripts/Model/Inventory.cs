using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(SlotsStorage))]
public class Inventory : MonoBehaviour
{
    [SerializeField] private int _slotCount;
    [SerializeField] private SlotsStorage _slotsStorage;
    [SerializeField] private GameObject _view;

    public SlotsStorage SlotsStorage => _slotsStorage;
    public GameObject InventoryPanel => _slotsStorage.SlotsPanel;

    public int SlotCount => _slotCount;

    private void Awake()
    {
        if (_view == null)
            throw new System.ArgumentNullException(nameof(_view));

        if (_slotsStorage == null)
            _slotsStorage = GetComponent<SlotsStorage>();

        if (_slotsStorage != null)
            _slotsStorage.CreateSlots(this);
    }

    public void AddItem(Item item, int quantity)
    {
        if (quantity < 0)
            throw new System.ArgumentOutOfRangeException(nameof(quantity));

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

            if (slot == null)
                return;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _view.SetActive(!_view.activeSelf);
            var mouseHandler = GetComponent<PlayerCursorHandler>();

            mouseHandler.ToggleCursorLock();
            Time.timeScale = !_view.activeSelf ? 1 : 0; ;
        }
    }

}
