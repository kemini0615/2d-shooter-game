using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform shootPosition;
    [SerializeField] protected WeaponData weaponData;

    public WeaponData WeaponData => weaponData;
    private Animator animator;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected void PlayShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    public virtual void Attack()
    {
        PlayShootAnimation();
    }
}
