using UnityEngine;

[CreateAssetMenu()]
public class PlayerConfiguration : ScriptableObject
{
    [Header("Meta")]
    public string playerName;
    public Sprite playerIcon;

    [Header("Stats")]
    public int level;
    public float currentHealth;
    public float maxHealth;
    public float armor;
    public float maxArmor;
    public float energy;
    public float MaxEnergy;
    public float criticalChance;
    public float criticalDamage;
}
