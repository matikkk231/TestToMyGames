using System.Collections.Generic;
using Project.Scripts.Area.Zombie;
using UnityEngine;

namespace Project.Scripts.Area.Round
{
    [CreateAssetMenu(menuName = "roundConfig")]
    public class RoundConfig : ScriptableObject
    {
        public float TimeBetweenZombieSpawn;
        public int MaxZombiesInGame;
        public List<ZombieConfig> ZombieConfigs;
    }
}