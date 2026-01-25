using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private PlayerConfiguration playerConfiguration;

    public bool CanUseEnergy => playerConfiguration.energy > 0f;

    /// <summary>
    /// 회복량만큼 에너지를 회복한다.
    /// </summary>
    /// <param name="energy">회복량</param>
    public void RecoverEnergy(float energy)
    {
        playerConfiguration.energy = Mathf.Min(playerConfiguration.energy + energy, playerConfiguration.MaxEnergy);
    }

    /// <summary>
    /// 사용량만큼 에너지를 깎는다.
    /// </summary>
    /// <param name="energy">사용량</param>
    public void UseEnergy(float energy)
    {
        playerConfiguration.energy = Mathf.Max(playerConfiguration.energy - energy, 0f);
    }

    // TEMP
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecoverEnergy(1f);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            UseEnergy(1f);
        }
    }
}
