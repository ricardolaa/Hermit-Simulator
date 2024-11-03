using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
public class Consumable : Item
{
    [SerializeField] private consumableType _typeOfConsumable;
 
    [SerializeField] private int _HPRecover;

    public override void Use()
    {
        base.Use();

        //Consumble Action
      
    }

    public enum consumableType { Potion, Food }
   
}
