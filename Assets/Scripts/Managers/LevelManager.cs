using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private RoomTemplates roomTemplates;
    [SerializeField] private DungeonReferences dungeonReferences;

    public RoomTemplates RoomTemplates => roomTemplates;
    public DungeonReferences DungeonReferences => dungeonReferences;


    private void Awake()
    {
        Instance = this;
    }
}
