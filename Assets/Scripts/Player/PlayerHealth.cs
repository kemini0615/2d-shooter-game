using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerConfiguration playerConfiguration;

    /// <summary>
    /// 회복량만큼 체력을 회복한다.
    /// </summary>
    /// <param name="health">회복량</param>
    public void RecoverHealth(float health)
    {
        playerConfiguration.currentHealth = Mathf.Min(playerConfiguration.currentHealth + health, playerConfiguration.maxHealth);
    }

    /// <summary>
    /// 공격을 받아서 대미지만큼 체력과 아머를 깎는다.
    /// </summary>
    /// <param name="damage">대미지</param>
    public void TakeDamage(float damage)
    {
        // 아머가 있다면
        if (playerConfiguration.armor > 0f)
        {
            // 아머를 먼저 깎는다.
            float remainingDamage = damage - playerConfiguration.armor;
            playerConfiguration.armor = Mathf.Max(playerConfiguration.armor - damage, 0f);

            // 아머를 깎고도 대미지가 남아있다면, 체력을 깎는다.
            if (remainingDamage > 0f)
            {
                playerConfiguration.currentHealth = Mathf.Max(playerConfiguration.currentHealth - remainingDamage, 0f);
            }

        }
        // 아머가 없다면
        else
        {
            // 체력을 깎는다.
            playerConfiguration.currentHealth = Mathf.Max(playerConfiguration.currentHealth - damage, 0f);
        }

        // 체력이 0 이하라면
        if (playerConfiguration.currentHealth <= 0f)
        {
            // 죽는다.
            Die();
        }
    }

    /// <summary>
    /// 플레이어를 씬에서 제거한다.
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }

    // TEMP
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RecoverHealth(1f);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(1f);
        }
    }
}