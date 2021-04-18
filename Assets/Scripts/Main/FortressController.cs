using System.Collections.Generic;
using Enemy;
using Field;
using Runtime;

namespace Main
{
    public class FortressController : IController
    {
        private Node m_TargetNode;

        private List<EnemyData> m_ReachedEnemyDatas = new List<EnemyData>();

        public FortressController(Grid grid)
        {
            m_TargetNode = grid.GetTargetNode();
        }

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
                if (enemyData.View.MovementAgent.GetCurrentNode() == m_TargetNode)
                {
                    enemyData.DamageFortress();
                    m_ReachedEnemyDatas.Add(enemyData);
                }
            }

            if (m_ReachedEnemyDatas.Count == 0)
            {
                return;
            }

            foreach (EnemyData enemyData in m_ReachedEnemyDatas)
            {
                Game.Player.EnemyReachedFortress(enemyData);
            }
            
            Game.Player.CheckForGameOver();
            
            m_ReachedEnemyDatas.Clear();
        }
    }
}