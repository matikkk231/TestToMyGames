using System;
using Project.Scripts.Area.Player.View;
using UnityEngine;

namespace Project.Scripts.Area.LevelManager.View
{
    public class LevelView : MonoBehaviour, ILevelView
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _floor;

        private void Awake()
        {
            for (int x = -30; x < 30; x += 10)
            {
                for (int z = -30; z < 30; z += 10)
                {
                    Instantiate(_floor).transform.position = new Vector3(x, 0, z);
                }
            }
        }

        public IPlayerView CreatePlayer()
        {
            var playerView = Instantiate(_playerPrefab).GetComponent<IPlayerView>();
            return playerView;
        }
    }
}