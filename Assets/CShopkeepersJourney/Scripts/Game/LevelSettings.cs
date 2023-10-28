using UnityEngine;
using System.Collections.Generic;

namespace com.vollmergames
{

    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Game/Level Settings")]
    public class LevelSettings : ScriptableObject
    {
        [Header("Level Information")]
        public string levelName;

        [Header("Time Limit (in seconds)")]
        public float timeLimit;

        [Header("Score per item")]
        public int scorePerItem = 10;

        [Header("Chinese Learning Items")]
        public List<ChineseLearningItem> learningItems = new List<ChineseLearningItem>();
    }

}
