using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonReferences", menuName = "Dungeon/Dungeon References")]
public class DungeonReferences : ScriptableObject
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
