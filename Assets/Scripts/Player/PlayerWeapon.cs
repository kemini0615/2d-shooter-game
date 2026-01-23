using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Weapon initialWeapon;
    [SerializeField] private Transform weaponPosition;

    private Weapon currentWeapon;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        InstantiateWeapon(initialWeapon);
    }

    void Update()
    {
        if (playerMovement.MoveDirection != Vector2.zero)
            RotateWeapon(playerMovement.MoveDirection);
    }

    private void InstantiateWeapon(Weapon weapon)
    {
        currentWeapon = Instantiate(weapon, weaponPosition.position, Quaternion.identity, weaponPosition);
    }

    private void RotateWeapon(Vector3 direction)
    {
        // 좌표의 라디안 각도를 반환한다.
        // 360도(degrees) = 2파이 라디안(radians)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // 오른쪽 방향.
        if (direction.x >= 0f)
        {
            weaponPosition.localScale = new Vector3(1f, 1f, 1f);
            currentWeapon.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        // 왼쪽 방향.
        else
        {
            weaponPosition.localScale = new Vector3(1f, 1f, 1f);
            currentWeapon.transform.localScale = new Vector3(1f, -1f, 1f);
        }

        currentWeapon.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }
}
