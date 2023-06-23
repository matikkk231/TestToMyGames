using System;

namespace Project.Scripts.Area.Zombie.Model
{
    public interface IZombieModel
    {
        public Action Died { get; set; }
        public Action<IZombieModel> Removed { get; set; }
        public Action<int> Attacked { get; set; }
        public Action<int, int> HealthChanged { get; set; }

        public int Health { get;set; }
        public int StartHealth { get; set; }
        public int DamageAmount { get; set; }

        public void GetDamage(int damage);
        public void Attack();
    }
}