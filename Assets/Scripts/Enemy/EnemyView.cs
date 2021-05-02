using UI.InGame.Overtips;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField]
        private EnemyOvertip m_Overtip;
        
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;

        public EnemyData Data => m_Data;
        public IMovementAgent MovementAgent => m_MovementAgent;

        public void AttachData(EnemyData data)
        {
            m_Data = data;
            m_Overtip.SetData(m_Data);
        }

        public void CreateMovementAgent(Grid grid)
        {
            m_MovementAgent = new GridMovementAgent(m_Data.Asset.Speed, transform, grid);
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void ReachedTarget()
        {
            Destroy(gameObject);
        }
    }
}