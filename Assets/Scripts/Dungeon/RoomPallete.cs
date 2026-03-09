using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomPallete", menuName = "Dungeon/Room Palette")]
public class RoomPallete : ScriptableObject
{
    public Texture2D[] templates;
    public PropData[] props;
}

[Serializable]
public struct PropData
{
    public string name;
    public GameObject prefab;
    public Color pixelColor; // 템플릿 이미지의 픽셀 컬러와 일치시킨다.
}
