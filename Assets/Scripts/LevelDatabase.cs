using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelDatabase", menuName = "Level Database", order = 2)]
public class LevelDatabase : ScriptableObject
{
    public List<LevelData> levels = new List<LevelData>();
}