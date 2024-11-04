using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class InventoryDataStr
{
    public List<Item> itemList;
    public List<int> intList;
}

public class SaveInventorySystem : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public void SaveInventory()
    {

        InventoryDataStr saveData = new InventoryDataStr();
        saveData.itemList = _inventory.ItemList;
        saveData.intList = _inventory.QuantityList;

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("InventoryData", json);
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey("InventoryData"))
        {
            string json = PlayerPrefs.GetString("InventoryData");
            InventoryDataStr saveData = JsonUtility.FromJson<InventoryDataStr>(json);

            FieldInfo itemListInfo = typeof(Inventory).GetField("_itemList", BindingFlags.Instance | BindingFlags.NonPublic);
            if (itemListInfo != null)
            {
                itemListInfo.SetValue(_inventory, saveData.itemList);
            }

            FieldInfo itemQuantityList = typeof(Inventory).GetField("_quantityList", BindingFlags.Instance | BindingFlags.NonPublic);
            if (itemQuantityList != null)
            {
                itemQuantityList.SetValue(_inventory, saveData.intList);
            }

            _inventory.SlotsStorageUI.UpdateAllSlots(new Inventory());
        }
    }

}
