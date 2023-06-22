using System;
using UnityEngine;

namespace Project.Scripts.Area.Zombie.View
{
    public interface IZombieView
    {
        Action TargetFound { get; set; }
        Action<int> Damaged { get; set; }
        Action<IZombieView> Removed { get; set; }
        public Vector3 Position { get; set; }
        public Transform TargetToChase { set; }

        public void SetActive(bool isActive);
        public void ChaseTarget();
        public void GetDamage(int damage);
        public void Attack(int damage);
    }
}