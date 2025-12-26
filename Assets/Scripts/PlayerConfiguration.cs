using UnityEngine;

[CreateAssetMenu()]
public class PlayerConfiguration : ScriptableObject
{
    [Header("Meta")]
    public string playerName;
    public Sprite playerIcon;

    [Header("Stats")]
    public int level;
    public int currentHealth;
    public int maxHealth;
    public int armor;
    public int maxArmor;
    public int energy;
    public int MaxEnergy;
    public int criticalChance;
    public int criticalDamage;
}
