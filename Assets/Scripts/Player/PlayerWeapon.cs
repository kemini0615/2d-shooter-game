using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Weapon initialWeapon;
    [SerializeField] private Transform weaponPosition;

    private Weapon currentWeapon;
    private PlayerEnergy playerEnergy;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerEnergy = GetComponent<PlayerEnergy>();
        playerInputActions = new PlayerInputActions();
    }

    void Start()
    {
        playerInputActions.Attack.Shoot.performed += ctx => TryAttack();
        InstantiateWeapon(initialWeapon);
    }

    void Update()
    {
        RotateWeapon();
    }

    /// <summary>
    /// 무기를 인스턴스화하고 장착한다.
    /// </summary>
    /// <param name="weapon">사용할 무기</param>
    private void InstantiateWeapon(Weapon weapon)
    {
        currentWeapon = Instantiate(weapon, weaponPosition.position, Quaternion.identity, weaponPosition);
    }

    /// <summary>
    /// 마우스의 위치에 따라 무기를 회전한다.
    /// </summary>
    private void RotateWeapon()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 direction = mouseWorldPosition - transform.position;

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

    /// <summary>
    /// 공격을 시도한다.
    /// 공격에 성공하면 에너지를 소모한다.
    /// </summary>
    private void TryAttack()
    {
        // 현재 무기가 없다면 종료.
        if (currentWeapon == null)
            return;

        // 공격이 불가능하다면 종료.
        if (!CanAttack())
            return;

        // 공격하고 에너지를 소모한다.
        currentWeapon.Attack();
        playerEnergy.UseEnergy(currentWeapon.WeaponData.energyCost);
    }

    /// <summary>
    /// 공격이 가능한 상태인지를 확인한다.
    /// </summary>
    /// <returns>공격 가능 여부</returns>
    private bool CanAttack()
    {
        // 무기 타입이 원거리이고 에너지가 남아있다면 공격 가능.
        if (currentWeapon.WeaponData.weaponType == WeaponType.Ranged && playerEnergy.CanUseEnergy)
            return true;

        // 무기 타입이 근거리라면 공격 가능.
        if (currentWeapon.WeaponData.weaponType == WeaponType.Melee)
            return true;

        return false;
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
