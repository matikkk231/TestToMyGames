using System;
using Project.Scripts.Base.AdoioService.View;

namespace Project.Scripts.Area.Gun.View
{
    public interface IGunView
    {
        Action AttackPressed { get; set; }
        public void Attack(int damage);
        public void AddAudioService(IAudioServiceView audioServiceView);
    }
}