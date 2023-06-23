using System;
using Project.Scripts.Area.Gun.View;
using Project.Scripts.Base.AdoioService.View;
using UnityEngine;

namespace Project.Scripts.Area.Player.View
{
    public interface IPlayerView
    {
        Action<int> Damaged { get; set; }
        Transform Transform { get; }
        IGunView Gun { get; }

        public void GetDamage(int damage);
        public void AddAudioService(IAudioServiceView audioServiceView);
        public void SetMovingArea(Vector3 minPosition, Vector3 maxPosition);
    }
}