using UnityEngine;

namespace Project.Scripts.Area.Zombie.View
{
    public interface IZombieView
    {
        public Transform TargetToChase { get; set; }

        public void ChaseTarget();
    }
}