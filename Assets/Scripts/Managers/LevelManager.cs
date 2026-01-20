using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    // TEMP
    [SerializeField] GameObject player;

    [SerializeField] private RoomPallete roomPallete;
    [SerializeField] private DungeonConfig dungeonConfig;

    public RoomPallete RoomPallete => roomPallete;
    public DungeonConfig DungeonConfig => dungeonConfig;

    private int currentLevelIndex = 0;
    private int currentDungeonIndex = 0;
    private GameObject currentDungeon;
    private Room currentRoom;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateDungeon();
    }

    /// <summary>
    /// 던전을 생성한다.
    /// </summary>
    private void GenerateDungeon()
    {
        currentDungeon = Instantiate(dungeonConfig.levels[currentLevelIndex].dungeons[currentDungeonIndex], transform);
    }

    /// <summary>
    /// 다음 던전으로 전환한다.
    /// </summary>
    private void TransitionToNextDungeon()
    {
        StartCoroutine(ProcessTransition());
    }

    /// <summary>
    /// 페이드 아웃, 페이드 인을 포함한 던전 트랜지션을 처리한다.
    /// </summary>
    private IEnumerator ProcessTransition()
    {
        UIManager.Instance.FadeScreen(1f); // Fade Out.

        yield return new WaitForSeconds(1f);

        LoadNextDungeon();
        UIManager.Instance.FadeScreen(0f); // Fade In.
    }

    /// <summary>
    /// 다음 던전을 생성하고 플레이어 위치를 초기화한다.
    /// </summary>
    private void LoadNextDungeon()
    {
        currentDungeonIndex++;

        if (currentDungeonIndex >= dungeonConfig.levels[currentLevelIndex].dungeons.Length)
        {
            currentDungeonIndex = 0;
            currentLevelIndex++;
        }

        Destroy(currentDungeon);
        GenerateDungeon();
        ResetPlayerPosition();
    }

    /// <summary>
    /// 플레이어 위치를 던전 입구로 이동시킨다.
    /// </summary>
    private void ResetPlayerPosition()
    {
        Room[] rooms = currentDungeon.GetComponentsInChildren<Room>();
        Room entranceRoom = null;

        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].RoomType == RoomType.Entrance)
            {
                entranceRoom = rooms[i];
                break;
            }
        }

        if (entranceRoom != null && player != null)
        {
            player.transform.position = entranceRoom.transform.position;
        }
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
        Portal.OnPlayerEnter += TransitionToNextDungeon;
    }

    private void OnDisable()
    {
        Room.OnPlayerEnter -= CloseAllDoorsInCurrentRoom;
        Portal.OnPlayerEnter -= TransitionToNextDungeon;
    }
}
