using System;
using Enemy;
using UnityEngine;

namespace Turret.Weapons.Projectiles
{
    public abstract class ProjectileBase<TProjectileAsset> : MonoBehaviour where TProjectileAsset : ProjectileAssetBase
    {
        protected TProjectileAsset Asset;
        protected EnemyData EnemyData;

        public void OnInstantiated(Vector3 origin, Vector3 originForward, EnemyData enemyData, TProjectileAsset asset)
        {
            Asset = asset;
            EnemyData = enemyData;
            transform.position = origin;
            transform.forward = originForward;
        }

        private void Update()
        {
            TickApproaching();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.gameObject.GetComponent<EnemyView>().Data == EnemyData)
                {
                    OnCollidedWithEnemy();
                }
            }
        }

        protected abstract void TickApproaching();

        protected abstract void OnCollidedWithEnemy();
    }
}