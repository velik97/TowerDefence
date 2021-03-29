using Assets;
using Runtime;
using UnityEngine;

namespace Enemy
{
    public class EnemyData
    {
        private EnemyView m_View;
        public EnemyView View => m_View;

        private int m_Health;
        private bool m_IsDead;
        public bool IsDead => m_IsDead;

        public EnemyData(EnemyAsset asset)
        {
            m_Health = asset.StartHealth;
        }

        public void AttachView(EnemyView view)
        {
            m_View = view;
            m_View.AttachData(this);
        }

        public void GetDamage(int damage)
        {
            m_Health -= damage;
            if (m_Health < 0)
            {
                Die();
            }
        }

        private void Die()
        {
            m_IsDead = true;
            Game.Player.EnemyDead(this);
            View.Die();
        }
    }
}