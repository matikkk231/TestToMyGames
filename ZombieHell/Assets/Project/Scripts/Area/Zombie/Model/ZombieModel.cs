using System;
using UnityEngine;

namespace Project.Scripts.Area.Zombie.Model
{
    public class ZombieModel : IZombieModel
    {
        public Action Died { get; set; }
        public Action<IZombieModel> Removed { get; set; }

        public int Health { get; set; }

        public ZombieModel()
        {
        }

        public void GetDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Died?.Invoke();
                Removed?.Invoke(this);
            }
        }
    }
}