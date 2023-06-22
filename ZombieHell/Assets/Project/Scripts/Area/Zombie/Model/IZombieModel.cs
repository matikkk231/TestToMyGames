using System;

namespace Project.Scripts.Area.Zombie.Model
{
    public interface IZombieModel
    {
        public Action Died { get; set; }
        public Action<IZombieModel> Removed { get; set; }

        public int Health { get; set; }

        public void GetDamage(int damage);
    }
}