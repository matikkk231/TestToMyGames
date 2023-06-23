using System;
using Project.Scripts.Area.Gun.Model;

namespace Project.Scripts.Area.Player.Model
{
    public class PlayerModel : IPlayerModel
    {
        public Action Died { get; set; }

        public int Health { get; set; } = 1;
        public IGunModel Gun { get; }

        public PlayerModel()
        {
            Gun = new GunModel();
            Gun.DamageAmount = 1;
        }

        public void GetDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Died?.Invoke();
            }
        }
    }
}