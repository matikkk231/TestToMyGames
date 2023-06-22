using System;
using Project.Scripts.Area.Gun.Model;

namespace Project.Scripts.Area.Player.Model
{
    public interface IPlayerModel
    {
        public Action Died { get; set; }
        public IGunModel Gun { get; }

        public void GetDamage(int damage);
    }
}