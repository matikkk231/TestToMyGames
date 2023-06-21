using System;
using Project.Scripts.Area.Player.Model;
using Project.Scripts.Area.Player.View;

namespace Project.Scripts.Area.Player.Presenter
{
    public class PlayerPresenter : IDisposable
    {
        private readonly IPlayerView _view;
        private readonly IPlayerModel _model;

        public PlayerPresenter(IPlayerView view, IPlayerModel model)
        {
            _model = model;
            _view = view;
        }

        public void Dispose()
        {
        }
    }
}