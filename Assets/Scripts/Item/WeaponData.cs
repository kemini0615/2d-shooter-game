using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged
}

public enum WeaponRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Items/Weapon Data")]
public class WeaponData : ItemData
{
    public WeaponType weaponType;
    public WeaponRarity weaponRarity;

    public float damage;
    public float energyCost;
    public float attackCooldown;
    public float minSpread;
    public float maxSpread;
}
