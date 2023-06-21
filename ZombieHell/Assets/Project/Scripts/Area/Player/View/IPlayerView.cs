using System;
using Project.Scripts.Area.Gun.View;
using UnityEngine;

namespace Project.Scripts.Area.Player.View
{
    public interface IPlayerView
    {
        Action<int> Damaged { get; set; }
        Transform Transform { get; }
        IGunView Gun { get; }

        public void GetDamage(int damage);
    }
}