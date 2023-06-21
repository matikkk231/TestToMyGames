using System;
using UnityEngine;

namespace Project.Scripts.Area.Zombie.View
{
    public interface IZombieView
    {
        Action<int> Damaged { get; set; }
        public Transform TargetToChase { set; }

        public void ChaseTarget();
        public void GetDamage(int damage);
    }
}