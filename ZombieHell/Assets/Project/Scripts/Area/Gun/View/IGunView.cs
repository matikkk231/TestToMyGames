using System;

namespace Project.Scripts.Area.Gun.View
{
    public interface IGunView
    {
        Action AttackPressed { get; set; }
        public void Attack(int damage);
    }
}