using System;

namespace Project.Scripts.Area.Gun.Model
{
    public interface IGunModel
    {
        public Action<int> Attacked { get; set; }
        int DamageAmount { get; set; }

        public void Attack();
    }
}