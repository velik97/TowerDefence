using System;
using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public class BulletProjectile : MonoBehaviour, IProjectile
    {
        private float m_Speed;
        private bool m_DidHit = false;
        private EnemyData m_HitEnemy = null;

        private Vector3 m_StartPosition;

        private const float MAX_SQR_DISTANCE = 200f * 200f;

        private void Awake()
        {
            m_StartPosition = transform.position;
        }

        public void SetSpeed(float speed)
        {
            m_Speed = speed;
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
            // if (m_HitEnemy != null)
            // {
            //     m_HitEnemy.Damage();
            // }
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}