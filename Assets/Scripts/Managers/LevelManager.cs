using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private RoomTemplates roomTemplates;
    [SerializeField] private DungeonReferences dungeonReferences;

    public RoomTemplates RoomTemplates => roomTemplates;
    public DungeonReferences DungeonReferences => dungeonReferences;

    private Room currentRoom;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 현재 던전 룸에 존재하는 모든 문을 닫는다.
    /// </summary>
    /// <param name="room">현재 던전 룸</param>
    private void CloseAllDoorsInCurrentRoom(Room room)
    {
        currentRoom = room;

        if (!currentRoom.Cleared)
        {
            currentRoom.CloseAllDoors();
        }
    }

    private void OnEnable()
    {
        Room.OnPlayerEnter += CloseAllDoorsInCurrentRoom;
    }

    private void OnDisable()
    {
        Room.OnPlayerEnter -= CloseAllDoorsInCurrentRoom;
    }
}
