using UnityEngine;

public class Prop : MonoBehaviour, IDamageable
{
    [SerializeField] private int durability;

    private int counter;

    public void TakeDamage()
    {
        counter++;

        if (counter >= durability)
        {
            Destroy(gameObject);
        }
    }
}
