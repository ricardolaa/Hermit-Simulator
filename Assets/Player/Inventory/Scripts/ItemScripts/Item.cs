using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField] private string _itemName;

    [SerializeField] private int _price;

    [SerializeField] private int _maxInStack;

    [SerializeField] private Sprite _itemIcon;

    public bool Stacable => _maxInStack > 1;
    public int MaxInStack => _maxInStack;
    public string Name => _itemName;
    public int Price => _price;
    public Sprite ItemIcon => _itemIcon;

    private void OnValidate()
    {
        if (_maxInStack > 99)
            _maxInStack = 99;

        if (_maxInStack < 1)
            _maxInStack = 1;
    }

    public virtual void Use()
    {
        //Use item
        //Use the following line if you want to destroy every item after use
        // Inventory.instance.RemoveItem(this, 1);
    }
}
