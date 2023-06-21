using System;

namespace Project.Scripts.Area.Gun.Model
{
    public class GunModel : IGunModel
    {
        public Action<int> Attacked { get; set; }
        public int DamageAmount { get; set; }

        public void Attack()
        {
            Attacked?.Invoke(DamageAmount);
        }
    }
}