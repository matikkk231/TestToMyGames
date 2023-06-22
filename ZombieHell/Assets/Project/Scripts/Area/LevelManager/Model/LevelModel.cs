using System;
using System.Collections.Generic;
using Project.Scripts.Area.Player.Model;
using Project.Scripts.Area.Zombie.Model;

namespace Project.Scripts.Area.LevelManager.Model
{
    public class LevelModel : ILevelModel
    {
        public Action<IZombieModel> ZombieRemoved { get; set; }

        private List<IZombieModel> _activeZombies = new List<IZombieModel>();
        private List<IZombieModel> _cachedZombies = new List<IZombieModel>();
        public IPlayerModel Player { get; private set; } = new PlayerModel();

        public IZombieModel GetNewZombie()
        {
            if (_cachedZombies.Count != 0)
            {
                var zombie = _cachedZombies[0];
                _cachedZombies.Remove(zombie);
                _activeZombies.Add(zombie);
                zombie.Removed += OnZombieRemoved;
                return zombie;
            }
            else
            {
                var zombie = new ZombieModel();
                _activeZombies.Add(zombie);
                zombie.Removed += OnZombieRemoved;
                return zombie;
            }
        }

        private void OnZombieRemoved(IZombieModel zombieModel)
        {
            ZombieRemoved?.Invoke(zombieModel);
            _activeZombies.Remove(zombieModel);
            _cachedZombies.Add(zombieModel);
        }

        public void StartLevel()
        {
            Player = new PlayerModel();
        }
    }
}