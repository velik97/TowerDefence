using System;
using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public class BulletProjectile : MonoBehaviour, IProjectile
    {
        private float m_Speed;
        private int m_Damage;
        private bool m_DidHit = false;
        private EnemyData m_HitEnemy = null;

        private Vector3 m_StartPosition;

        private const float MAX_SQR_DISTANCE = 200f * 200f;

        private void Awake()
        {
            m_StartPosition = transform.position;
        }

        public void SetAsset(BulletProjectileAsset asset)
        {
            m_Speed = asset.Speed;
            m_Damage = asset.Damage;
        }

        public void TickProjectile()
        {
            transform.Translate(transform.forward * (m_Speed * Time.deltaTime), Space.World);
            if ((m_StartPosition - transform.position).sqrMagnitude > MAX_SQR_DISTANCE)
            {
                m_DidHit = true;
            }
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
            m_HitEnemy?.GetDamage(m_Damage);
            Destroy(gameObject);
        }
    }
}