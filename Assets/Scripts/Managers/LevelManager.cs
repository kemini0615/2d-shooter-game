using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private RoomTemplates roomTemplates;
    public RoomTemplates RoomTemplates => roomTemplates;

    private void Awake()
    {
        Instance = this;
    }
}
