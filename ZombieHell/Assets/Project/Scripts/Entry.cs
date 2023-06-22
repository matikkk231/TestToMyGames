using System;
using System.Collections.Generic;
using Project.Scripts.Area.LevelManager.Model;
using Project.Scripts.Area.LevelManager.Presenter;
using Project.Scripts.Area.LevelManager.View;
using Project.Scripts.Area.MainMenu.View;
using Project.Scripts.Area.Round;
using UnityEngine;

namespace Project.Scripts
{
    public class Entry : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject _menuPrefab;
        [SerializeField] private GameObject _levelManagerPrefab;
        [SerializeField] private List<RoundConfig> _roundConfigs;
        private ILevelModel _levelModel;
        private IMainMenuView _mainMenuView;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        private void Awake()
        {
            _mainMenuView = Instantiate(_menuPrefab).GetComponent<IMainMenuView>();
            var levelView = Instantiate(_levelManagerPrefab).GetComponent<ILevelView>();
            var levelModel = new LevelModel(_roundConfigs);
            _levelModel = levelModel;
            _disposables.Add(new LevelPresenter(levelView, levelModel));
            AddListeners();
            levelModel.StartLevel();
        }

        private void AddListeners()
        {
            _levelModel.LevelWon += OnLevelWon;
            _levelModel.LevelLoosed += OnLevelLoosed;
        }


        private void OnLevelWon()
        {
            _mainMenuView.ShowWinNotify();
        }

        private void OnLevelLoosed()
        {
            _mainMenuView.ShowLooseNotify();
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