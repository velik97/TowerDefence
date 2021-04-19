using Runtime;

namespace Main
{
    public class WinController : IController
    {
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            if (!Game.Player.AllWavesAreSpawned)
            {
                return;
            }

            if (Game.Player.EnemyDatas.Count == 0)
            {
                Game.Player.GameWon();
            }
        }
    }
}