using System;
using System.Collections.Generic;
using Project.Scripts.Area.LevelManager.Model;
using Project.Scripts.Area.LevelManager.Presenter;
using Project.Scripts.Area.LevelManager.View;
using Project.Scripts.Area.Round;
using UnityEngine;

namespace Project.Scripts
{
    public class Entry : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject _menuPrefab;
        [SerializeField] private GameObject _levelManagerPrefab;
        [SerializeField] private List<RoundConfig> _roundConfigs;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        private void Awake()
        {
            Instantiate(_menuPrefab);
            var levelView = Instantiate(_levelManagerPrefab).GetComponent<ILevelView>();
            var levelModel = new LevelModel(_roundConfigs);
            _disposables.Add(new LevelPresenter(levelView, levelModel));
            levelModel.StartLevel();
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}