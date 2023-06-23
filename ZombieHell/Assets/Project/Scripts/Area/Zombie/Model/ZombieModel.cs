using System;

namespace Project.Scripts.Area.Zombie.Model
{
    public class ZombieModel : IZombieModel
    {
        public Action Died { get; set; }
        public Action<IZombieModel> Removed { get; set; }
        public Action<int> Attacked { get; set; }
        public Action<int, int> HealthChanged { get; set; }

        private int _startHealth;
        private int _currentHealth;
        private bool isSetHealthFirstTime = true;

        public int Health
        {
            get => _currentHealth;
            set
            {
                if (isSetHealthFirstTime)
                {
                    _startHealth = value;
                    _currentHealth = _startHealth;
                    isSetHealthFirstTime = false;
                    HealthChanged?.Invoke(_currentHealth, _startHealth);
                    return;
                }

                _currentHealth = value;
                HealthChanged?.Invoke(_currentHealth, _startHealth);
            }
        }

        public int StartHealth
        {
            get => _startHealth;
            set => _startHealth = value;
        }

        public int DamageAmount { get; set; }

        public void GetDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Died?.Invoke();
                Removed?.Invoke(this);
            }
        }

        public void Attack()
        {
            Attacked?.Invoke(DamageAmount);
        }
    }
}