using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("TEMP")]
    [SerializeField] private PlayerConfiguration playerConfiguration;

    [Header("Player UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image armorBar;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private Image energyBar;
    [SerializeField] private TextMeshProUGUI energyText;

    private void Update()
    {
        UpdatePlayerUI();
    }

    private void UpdatePlayerUI()
    {
        // 슬라이드 바 UI 업데이트.
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerConfiguration.currentHealth / playerConfiguration.maxHealth, 10f * Time.deltaTime);
        armorBar.fillAmount = Mathf.Lerp(armorBar.fillAmount, playerConfiguration.armor / playerConfiguration.maxArmor, 10f * Time.deltaTime);
        energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, playerConfiguration.energy / playerConfiguration.MaxEnergy, 10f * Time.deltaTime);

        // 텍스트 UI 업데이트.
        healthText.text = $"{playerConfiguration.currentHealth}/{playerConfiguration.maxHealth}";
        armorText.text = $"{playerConfiguration.armor}/{playerConfiguration.maxArmor}";
        energyText.text = $"{playerConfiguration.energy}/{playerConfiguration.MaxEnergy}";
    }
}
