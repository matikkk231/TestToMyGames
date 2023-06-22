using UnityEngine;

namespace Project.Scripts.Area.Zombie
{
    [CreateAssetMenu(menuName = "zombieConfig")]
    public class ZombieConfig : ScriptableObject
    {
        public int Health;
        public int DamageAmount;
    }
}