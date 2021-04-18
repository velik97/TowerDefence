using UnityEngine;
using Utils.Pooling;
using Grid = Field.Grid;

namespace Enemy
{
    public class EnemyView : PooledMonoBehaviour
    {
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;

        public EnemyData Data => m_Data;
        public IMovementAgent MovementAgent => m_MovementAgent;

        public override void AwakePooled()
        {
            // todo spawn particles
        }

        public void AttachData(EnemyData data)
        {
            m_Data = data;
        }

        public void CreateMovementAgent(Grid grid)
        {
            m_MovementAgent = new GridMovementAgent(m_Data.Asset.Speed, transform, grid);
        }

        public void Die()
        {
            GameObjectPool.ReturnObjectToPool(this);
        }
    }
}