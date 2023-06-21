using UnityEngine;

namespace Project.Scripts.Area.Player.View
{
    public interface IPlayerView
    {
        Transform Transform { get; }

        public void GetDamage(int damage);
    }
}