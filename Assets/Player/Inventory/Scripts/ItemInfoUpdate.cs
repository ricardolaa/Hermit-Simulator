using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUpdate : MonoBehaviour
{
    [SerializeField] private GameObject _infoPanel;

    [SerializeField] private Text _nameText;
    [SerializeField] private Image _icon;

    public void UpdateInfoPanel(Item itemInfo)
    {
        if (itemInfo != null)
        {
            _infoPanel.SetActive(true);

            _nameText.text = itemInfo.Name;
            _icon.sprite = itemInfo.ItemIcon;
        }
        else
        {
            _infoPanel.SetActive(false);
        }
    }

    public void ClosePanel()
    {
        _infoPanel.SetActive(false);
    }
}
