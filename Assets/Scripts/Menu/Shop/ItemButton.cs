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
    [SerializeField] bool _bought;
    public bool _select;

    public void SetItem(ShopItem item,Shop shop)
    {
        _shop = shop;
        _shopItem = item;
        _button = this.GetComponent<Button>();
        _bought = item.bought;
        _select = item.select;
        _nameText.text = item.itemName;
        _costCredits = item.cost;
        _costText.text = _costCredits.ToString();
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
            if (_select)
            {
                _check.SetActive(true);
            }
            else
            {
                _check.SetActive(false);
            }
        }
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
            CheckActive();
        }

        if (_select)
        {
            CheckDiseable();
        }

        _shop.UpdateCurrentCredits();
        _shopItem.UpdateValues(_costCredits,_bought,_select);
    }

    public void CheckActive()
    {
        _select = true;
        _check.SetActive(true);
        _shop.CheckForCheckeables();
    }
    public void CheckDiseable()
    {
        _select = false;
        _check.SetActive(false);
    }
}
