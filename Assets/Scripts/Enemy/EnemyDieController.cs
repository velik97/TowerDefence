using System.Collections.Generic;
using Runtime;

namespace Enemy
{
    public class EnemyDieController : IController
    {
        List<EnemyData> m_DiedEnemies = new List<EnemyData>();

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            foreach (EnemyData enemyData in Game.Player.EnemyDatas)
            {
                if (enemyData.Health <= 0)
                {
                    enemyData.Die();
                    m_DiedEnemies.Add(enemyData);
                }
            }
            
            foreach (EnemyData enemyData in m_DiedEnemies)
            {
                Game.Player.EnemyDied(enemyData);
            }
            m_DiedEnemies.Clear();
        }
    }
}