using System.Collections.Generic;
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
    [SerializeField] private bool isDebugging;
    [SerializeField] private RoomType roomType;
    [SerializeField] private Tilemap extraLayer;

    /// <summary>
    /// 타일맵에서 프롭을 배치할 수 있는 위치(Vector3)를 저장하는 딕셔너리
    /// </summary>
    private Dictionary<Vector3, bool> tiles = new Dictionary<Vector3, bool>();

    private void Start()
    {
        SetTiles();
        GeneratePropsBasedOnTemplate();
    }

    /// <summary>
    /// 타일맵에 존재하는 모든 타일의 셀 포지션을 월드 포지션으로 변환해서 tiles 딕셔너리에 저장한다.
    /// </summary>
    private void SetTiles()
    {
        if (IsEmpty())
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
        if (IsEmpty())
            return;

        int randomIndex = Random.Range(0, LevelManager.Instance.RoomTemplates.templates.Length);
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
    /// 현재 던전 룸이 입구(Entrance) 또는 빈 방(Empty)인지 확인한다. 
    /// </summary>
    private bool IsEmpty()
    {
        return (roomType == RoomType.Entrance || roomType == RoomType.Empty);
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
