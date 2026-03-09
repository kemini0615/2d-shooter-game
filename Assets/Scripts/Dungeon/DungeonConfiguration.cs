using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonConfiguration", menuName = "Dungeon/Dungeon Configuration")]
public class DungeonConfiguration : ScriptableObject
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
