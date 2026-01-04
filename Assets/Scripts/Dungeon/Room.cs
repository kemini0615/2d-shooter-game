using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum RoomType
{
    Empty,
    Entrance,
    Battle,
    Boss,
}

public class Room : MonoBehaviour
{
    public static Action<Room> OnPlayerEnter;

    [SerializeField] private bool isDebugging;
    [SerializeField] private RoomType roomType;
    [SerializeField] private Tilemap extraLayer;

    public RoomType RoomType => roomType;

    [SerializeField] private Transform[] doorPositionsNS;
    [SerializeField] private Transform[] doorPositionsWE;

    public bool Cleared { get; set; } = false;

    /// <summary>
    /// 타일맵에서 프롭을 배치할 수 있는 위치(Vector3)를 저장하는 딕셔너리
    /// </summary>
    private Dictionary<Vector3, bool> tiles = new Dictionary<Vector3, bool>();

    /// <summary>
    /// 던전 룸에 생성할 문을 저장하는 리스트
    /// </summary>
    private List<Door> doors = new List<Door>();

    private void Start()
    {
        SetTiles();
        GeneratePropsBasedOnTemplate();
        GenerateDoors();
    }

    /// <summary>
    /// 타일맵에 존재하는 모든 타일의 셀 포지션을 월드 포지션으로 변환해서 tiles 딕셔너리에 저장한다.
    /// </summary>
    private void SetTiles()
    {
        if (IsEmptyRoom())
            return;

        // ExtraLayer 타일맵에 배치된 모든 타일을 대상으로 루프를 실행한다.
        foreach (Vector3Int cellPosition in extraLayer.cellBounds.allPositionsWithin)
        {
            // 타일의 셀 포지션을 월드 포지션으로 변환한다.
            Vector3 worldPosition = extraLayer.CellToWorld(cellPosition);
            // X축과 Y축 방향으로 0.5 만큼 이동한 곳이 타일의 피벗 포지션이 된다.
            Vector3 pivotPosition = worldPosition + new Vector3(0.5f, 0.5f, 0f);

            tiles.Add(pivotPosition, true);
        }
    }

    /// <summary>
    /// 템플릿 이미지를 토대로 던전 룸 안에 프롭을 생성한다.
    /// </summary>
    private void GeneratePropsBasedOnTemplate()
    {
        if (IsEmptyRoom())
            return;

        int randomIndex = UnityEngine.Random.Range(0, LevelManager.Instance.RoomTemplates.templates.Length);
        Texture2D template = LevelManager.Instance.RoomTemplates.templates[randomIndex];
        List<Vector3> positions = new List<Vector3>(tiles.Keys);

        // 템플릿의 모든 픽셀을 대상으로 루프를 실행한다.
        for (int y = 0, i = 0; y < template.height; y++)
        {
            for (int x = 0; x < template.width; x++, i++)
            {
                Color pixelColor = template.GetPixel(x, y);

                foreach (Prop prop in LevelManager.Instance.RoomTemplates.props)
                {
                    if (pixelColor == prop.pixelColor)
                    {
                        GameObject propInstance = Instantiate(prop.prefab, extraLayer.transform);
                        propInstance.transform.position = new Vector3(positions[i].x, positions[i].y, 0f);
                        if (tiles.ContainsKey(positions[i]))
                        {
                            tiles[positions[i]] = false;
                        }
                            
                    }
                }
            }
        }
    }

    /// <summary>
    /// 던전 룸 안에 출입구가 있다면 문을 생성한다.
    /// </summary>
    private void GenerateDoors()
    {
        if (doorPositionsNS.Length > 0)
        {
            for (int i = 0; i < doorPositionsNS.Length; i++)
            {
                InstantiateDoors(LevelManager.Instance.DungeonReferences.DoorNS, doorPositionsNS[i]);
            }
        }

        if (doorPositionsWE.Length > 0)
        {
            for (int i = 0; i < doorPositionsWE.Length; i++)
            {
                InstantiateDoors(LevelManager.Instance.DungeonReferences.DoorWE, doorPositionsWE[i]);
            }
        }
    }

    /// <summary>
    /// 던전 룸 안에 문을 생성하고 doors 리스트에 저장한다.
    /// </summary>
    private void InstantiateDoors(GameObject prefab, Transform transform)
    {
        GameObject door = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        doors.Add(door.GetComponent<Door>());
    }

    /// <summary>
    /// 던전 룸의 모든 문을 연다.
    /// </summary>
    public void OpenAllDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].PlayOpenAnimation();
        }
       
    }

    /// <summary>
    /// 던전 룸의 모든 문을 닫는다.
    /// </summary>
    public void CloseAllDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].PlayCloseAnimation();
        }
       
    }

    /// <summary>
    /// 현재 던전 룸이 입구(Entrance) 또는 빈 방(Empty)인지 확인한다. 
    /// </summary>
    private bool IsEmptyRoom()
    {
        return (roomType == RoomType.Entrance || roomType == RoomType.Empty);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsEmptyRoom())
            return;

        if (other.CompareTag("Player"))
        {
            if (OnPlayerEnter != null)
            {
                OnPlayerEnter.Invoke(this);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!isDebugging)
            return;

        if (tiles.Count > 0)
        {
            foreach (KeyValuePair<Vector3, bool> tile in tiles)
            {
                if (tile.Value)
                {
                    // 프롭 배치 가능한 상태.
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(tile.Key, Vector3.one * 0.8f);
                }
                else
                {
                    // 프롭 배치 불가능한 상태.
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(tile.Key, 0.3f);
                }
            }
        }
    }
}
