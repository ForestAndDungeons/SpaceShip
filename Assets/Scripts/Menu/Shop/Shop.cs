using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] ItemButton _itemPrefab = null;
    [SerializeField] Transform _gridGroupSkin = null;
    [SerializeField] Transform _gridGroupPU = null;
    [SerializeField] ShopItem[] _itemListSkin = new ShopItem[0];
    [SerializeField] ShopItem[] _itemListPowerUp = new ShopItem[0];
    [SerializeField] List<ItemButton> _itemListSK = new List<ItemButton>();
    [SerializeField] TextMeshProUGUI _currentCredit;

    private void Start()
    {
        _currentCredit.text = GameManager.Instance.GetCredits().ToString();
        for (int i = 0; i < _itemListSkin.Length; i++)
        {
            var newItem = Instantiate(_itemPrefab, _gridGroupSkin);
            newItem.SetItem(_itemListSkin[i],this);
            _itemListSK.Add(newItem);
        }

        for (int i = 0; i < _itemListPowerUp.Length; i++)
        {
            var newItem = Instantiate(_itemPrefab, _gridGroupPU);
            newItem.SetItem(_itemListPowerUp[i],this);
        }
        CheckForCheckeables();
    }

    public void UpdateCurrentCredits()
    {
        _currentCredit.text = GameManager.Instance.GetCredits().ToString();
    }

    public void CheckForCheckeables()
    {
        int checkeds = 0;
        foreach (var itemSK in _itemListSK)
        {
            Debug.Log(itemSK.name + " " + itemSK._select);
            if (itemSK._select)
            {
                checkeds++;
                if (checkeds > 1)
                {
                    itemSK.CheckDiseable();
                }
                Debug.Log(itemSK.name + "Esta seleccionado");
            }
        }
        
    }
}
