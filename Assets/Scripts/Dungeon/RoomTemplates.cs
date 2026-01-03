using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomTemplates", menuName = "Dungeon/Room Templates")]
public class RoomTemplates : ScriptableObject
{
    public Texture2D[] templates;
    public Prop[] props;
}

[Serializable]
public struct Prop
{
    public string name;
    public GameObject prefab;
    public Color pixelColor; // 템플릿 이미지의 픽셀 컬러와 일치시킨다.
}
