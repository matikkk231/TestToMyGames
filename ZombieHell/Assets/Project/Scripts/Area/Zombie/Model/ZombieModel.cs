using System;

namespace Project.Scripts.Area.Zombie.Model
{
    public class ZombieModel : IZombieModel
    {
        public Action Died { get; set; }
        public Action Damaged { get; set; }
        
        public int Health { get; set; }
    }
}