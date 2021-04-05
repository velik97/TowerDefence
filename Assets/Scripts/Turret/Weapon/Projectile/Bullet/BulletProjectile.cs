﻿using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile.Bullet
{
    public class BulletProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField]
        private GameObject m_HitFX;
        
        private float m_Speed;
        private float m_Damage = 5;
        private bool m_DidHit = false;
        private EnemyData m_HitEnemy = null;
        
        public void SetSpeed(float speed)
        {
            m_Speed = speed;
        }
        
        public void SetDamage(float damage)
        {
            m_Damage = damage;
        }
        
        public void TickApproaching()
        {
            transform.Translate(transform.forward * (m_Speed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            m_DidHit = true;
            if (other.CompareTag("Enemy"))
            {
                EnemyView enemyView = other.GetComponent<EnemyView>();
                if (enemyView != null)
                {
                    m_HitEnemy = enemyView.Data;
                }
            }
        }

        public bool DidHit()
        {
            return m_DidHit;
        }

        public void DestroyProjectile()
        {
            Instantiate(m_HitFX, transform.position, Quaternion.identity);
            if (m_HitEnemy != null)
            {
                m_HitEnemy.GetDamage(m_Damage);
            }
            Destroy(gameObject);
        }
    }
}