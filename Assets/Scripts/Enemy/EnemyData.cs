﻿using Assets;
using Runtime;
using UnityEngine;

namespace Enemy
{
    public class EnemyData
    {
        private EnemyView m_View;
        private EnemyAsset m_Asset;
        
        private float m_Health;
        private bool m_IsDead = false;

        public EnemyView View => m_View;
        public EnemyAsset Asset => m_Asset;
        public bool IsDead => m_IsDead;
        public float Health => m_Health;

        public EnemyData(EnemyAsset asset)
        {
            m_Asset = asset;
            m_Health = asset.StartHealth;
        }

        public void AttachView(EnemyView view)
        {
            m_View = view;
            m_View.AttachData(this);
        }

        public void GetDamage(float damage)
        {
            if (m_IsDead)
            {
                return;
            }
            m_Health -= damage;
        }

        public void Die()
        {
            m_IsDead = true;
            m_View.Die();
        }
    }
}