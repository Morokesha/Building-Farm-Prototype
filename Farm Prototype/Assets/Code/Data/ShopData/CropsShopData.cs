using System;
using Code.Data.GardenData;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Data.ShopData
{
    [Serializable]
    public class CropsShopData : ScriptableObject

    {
    public ProductType ProductType;

    [Header("DisplayData")] 
    public Image Logo;

    public string NameItem;

    public PriceData PriceData;
    }
}