using Enemy;
using UnityEngine;
using Utils.Pooling;

namespace Turret.Weapon.Projectile
{
    [CreateAssetMenu(menuName = "Assets/Bullet Projectile Asset", fileName = "Bullet Projectile Asset")]
    public class BulletProjectileAsset : ProjectileAssetBase
    {
        [SerializeField]
        private BulletProjectile m_BulletProjectile;

        public float Speed;
        public int Damage;
        
        public override IProjectile CreateProjectile(Vector3 origin, Vector3 originForward, EnemyData enemyData)
        {
            BulletProjectile bullet = GameObjectPool.InstantiatePooled(m_BulletProjectile, origin, Quaternion.LookRotation(originForward, Vector3.up));
            bullet.SetAsset(this);
            
            return bullet;
        }
    }
}