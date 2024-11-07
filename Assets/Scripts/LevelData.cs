using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class CandyGoal
    {
        public int level;
        public int quantity;
    }

    public List<CandyGoal> goals = new List<CandyGoal>();
}