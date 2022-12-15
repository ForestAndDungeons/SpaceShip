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
    [SerializeField] List<ItemButton> _itemListPW = new List<ItemButton>();
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
            _itemListPW.Add(newItem);
        }
        
    }

    private void Update()
    {
        CheckForCheckeables(_itemListSK,_itemListPW);
    }

    public void UpdateCurrentCredits()
    {
        _currentCredit.text = GameManager.Instance.GetCredits().ToString();
    }

    public void CheckForCheckeables(List<ItemButton> skinList,List<ItemButton> powerUpList )
    {
        foreach (var item in skinList)
        {
            if (GameManager.Instance.skinCheckID == item.id)
            {
                item.CheckActive(true);
                GameManager.Instance.playerMesh = item.skinMesh;
                GameManager.Instance.playerMaterial = item.skinMaterial;
            }
            else
            {
                item.CheckActive(false);
            }
        }
        foreach (var item in powerUpList)
        {
            if (GameManager.Instance.powerUpCheckID == item.id)
            {
                item.CheckActive(true);
                if (item.isSinuous == true)
                {
                    GameManager.Instance.defaultBull = false;
                    GameManager.Instance.isRandomBull = false;
                    GameManager.Instance.isSinuousBull = true;
                }
                else if (item.isRandom == true)
                {
                    GameManager.Instance.defaultBull = false;
                    GameManager.Instance.isSinuousBull = false;
                    GameManager.Instance.isRandomBull = true;
                }
                else
                {
                    GameManager.Instance.isSinuousBull = false;
                    GameManager.Instance.isRandomBull = false;
                    GameManager.Instance.defaultBull = true;
                }
            }
            else
            {
                item.CheckActive(false);
            }
        }
    }
}