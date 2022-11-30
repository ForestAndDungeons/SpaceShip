using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Shop/Item")]
public class ShopItem : ScriptableObject
{
    public GameObject itemPrefab;
    public int id;
    public string itemName;
    public int cost;
    public bool isPowerUP;
    public bool isBought;
    public Mesh mesh;
    public Material material;
    public bool isSinuous;
    public bool isRandom;
}
