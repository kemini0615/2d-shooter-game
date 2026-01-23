using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private Bullet bulletPrefab;

    public override void Attack()
    {
        base.Attack();

        PlayerShootAnimation();
        Shoot();
    }

    private void Shoot()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);

        bulletInstance.Direction = shootPosition.right;
        float randomSpread = Random.Range(weaponData.minSpread, weaponData.maxSpread);
        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, randomSpread));
    }
}
