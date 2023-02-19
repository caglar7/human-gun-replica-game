using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAME
{
    [CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
    public class LevelData : ScriptableObject
    {
        // load to level, update level counts
        public int level = 1;
        public int fakeLevel = 1;

        // set value on level complete event
        public int moneyGainInLevel = 0;
    }
}
