using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonConfig", menuName = "Dungeon/Dungeon Config")]
public class DungeonConfig : ScriptableObject
{
    public Level[] levels;

    public GameObject DoorNS;
    public GameObject DoorWE;
}

[Serializable]
public struct Level
{
    public string name;
    public GameObject[] dungeons;
}
