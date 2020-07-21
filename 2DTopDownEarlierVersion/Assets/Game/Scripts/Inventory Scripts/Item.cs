using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Weapon,
        HealthPotion,
        ManaPotion,
        Coin,
        Medkit,
    }

    public ItemType itemType;
    public int Amount;


    public Sprite GetSprite()
    {

        switch (itemType)
        {
            default:
            case ItemType.Weapon: return ItemAssets.Instance.weaponSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion: return ItemAssets.Instance.manaPotionSprite;
            case ItemType.Coin: return ItemAssets.Instance.coinSprite;
            case ItemType.Medkit: return ItemAssets.Instance.medkitSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
                return true;
            case ItemType.Weapon:
            case ItemType.Medkit:
                return false;
        }
    }
}
