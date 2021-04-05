using System;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;

        private Animator m_Animator;
        [SerializeField]
        private GameObject m_DestroyFx;
        private static readonly int DieAnimationIndex = Animator.StringToHash("Die");

        public EnemyData Data => m_Data;
        public IMovementAgent MovementAgent => m_MovementAgent;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
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
            m_Animator.SetTrigger(DieAnimationIndex);
            foreach (Collider coll in GetComponentsInChildren<Collider>())
            {
                Destroy(coll);
            }
            Invoke(nameof(DestroyEnemy), 1f);
        }

        private void DestroyEnemy()
        {
            Instantiate(m_DestroyFx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}