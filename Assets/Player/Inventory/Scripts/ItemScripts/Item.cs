using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField] private string _itemName;

    [SerializeField] private int _price;

    [SerializeField] private bool _stackable;

    [SerializeField] private Sprite _itemIcon;

    public bool Stacable => _stackable;
    public string Name => _itemName;
    public int Price => _price;
    public Sprite ItemIcon => _itemIcon;
    
    public virtual void Use()
    {
        //Use item
        //Use the following line if you want to destroy every item after use
        // Inventory.instance.RemoveItem(this, 1);
    }
}
