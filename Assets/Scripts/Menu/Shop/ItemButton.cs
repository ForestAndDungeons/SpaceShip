using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    Shop _shop;
    ShopItem _shopItem;
    Button _button;
    [SerializeField] GameObject _itemPref;
    [SerializeField] TextMeshProUGUI _costText;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] int _costCredits;
    [SerializeField] Image _coinImage;
    [SerializeField] GameObject _check;
    public Mesh skinMesh;
    public Material skinMaterial;
    [SerializeField] bool _bought;
    [SerializeField] bool _isPowerUp;
    public int id;
    public bool isSinuous;
    public bool isRandom;

    public void SetItem(ShopItem item,Shop shop)
    {
        
       if (GameManager.Instance.boughtItemsID.Contains(item.id))
        {
            _bought = true;
        }
        else { _bought = false; }
        isSinuous = item.isSinuous;
        isRandom = item.isRandom;
        _isPowerUp = item.isPowerUP;
        id = item.id;
        skinMesh = item.mesh;
        skinMaterial = item.material;
        _shop = shop;
        _shopItem = item;
        _button = this.GetComponent<Button>();
        _nameText.text = item.itemName;
        _costCredits = item.cost;
        _itemPref = item.itemPrefab;
        var NewItem = Instantiate(_itemPref,this.transform);
        if (_bought)
        {
            var colorButton = _button.colors;
            colorButton.normalColor = Color.black;
            colorButton.selectedColor = Color.black;
            colorButton.highlightedColor = Color.black;
            colorButton.pressedColor = Color.black;
            GetComponent<Button>().colors = colorButton;
            _costCredits = 0;
        }
        else
        {
            _costCredits = item.cost;
        }
        _costText.text = _costCredits.ToString();
    }

    public void ChangeValues()
    {
        if (!_bought)
        {
            if(GameManager.Instance.GetCredits() >= _costCredits)
            {
                var calculeteCost = GameManager.Instance.GetCredits() - _costCredits;
                GameManager.Instance.SetCredits(calculeteCost);
                _bought = true;
                GameManager.Instance.boughtItemsID.Add(_shopItem.id);
                GameManager.Instance.GetJSONManager()._data.boughtItemsID = GameManager.Instance.boughtItemsID.ToArray();
                _costCredits = 0;
                _costText.text = _costCredits.ToString();
                var colorButton = _button.colors;
                colorButton.normalColor = Color.black;
                colorButton.selectedColor = Color.black;
                colorButton.highlightedColor = Color.black;
                colorButton.pressedColor = Color.black;
                GetComponent<Button>().colors = colorButton;
            }
        }
        else
        {
            if (!_isPowerUp)
            {
                GameManager.Instance.skinCheckID = _shopItem.id;
            }
            else
            {
                GameManager.Instance.powerUpCheckID = _shopItem.id;
            }
            
        }

        _shop.UpdateCurrentCredits();
    }

    public void CheckActive(bool var)
    {
        _check.SetActive(var);
       
    }
}
