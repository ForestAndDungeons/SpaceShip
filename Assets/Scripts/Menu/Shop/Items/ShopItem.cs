using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Shop/Item")]
public class ShopItem : ScriptableObject
{
    public GameObject itemPrefab;
    public string itemName;
    public int cost;
    public bool bought;
    public bool select;


    public void UpdateValues(int newCost, bool newBought, bool newSelect)
    {
        cost = newCost;
        bought = newBought;
        select = newSelect;
    }
}
