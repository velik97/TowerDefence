using System.Collections.Generic;
using Runtime;

namespace Enemy
{
    public class EnemyDieController : IController
    {
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            List<EnemyData> deadEnemies = new List<EnemyData>();
            foreach (EnemyData enemyData in Game.Player.EnemyDatas)
            {
                if (enemyData.Health <= 0)
                {
                    deadEnemies.Add(enemyData);
                }
            }
            
            foreach (EnemyData deadEnemy in deadEnemies)
            {
                Game.Player.EnemyDied(deadEnemy);
                deadEnemy.Die();
            }
        }
    }
}