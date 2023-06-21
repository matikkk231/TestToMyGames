using System;
using Project.Scripts.Area.Zombie.Model;
using Project.Scripts.Area.Zombie.View;

namespace Project.Scripts.Area.Zombie.Presenter
{
    public class ZombiePresenter : IDisposable
    {
        public ZombiePresenter(IZombieView view, IZombieModel model)
        {
        }

        public void Dispose()
        {
        }
    }
}