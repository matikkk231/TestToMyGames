using System;

namespace Project.Scripts.Area.Player.Model
{
    public class PlayerModel : IPlayerModel
    {
        public Action Died { get; set; }
        public Action Damaged { get; set; }

        public int Health { get; set; }
    }
}