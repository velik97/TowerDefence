using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    [CreateAssetMenu(menuName = "Assets/Bullet Projectile Asset", fileName = "Bullet Projectile Asset")]
    public class BulletProjectileAsset : ProjectileAssetBase
    {
        [SerializeField]
        private BulletProjectile m_BulletProjectile;

        [SerializeField]
        private float m_Speed;
        
        public override IProjectile CreateProjectile(Vector3 origin, Vector3 originForward, EnemyData enemyData)
        {
            BulletProjectile bullet = Instantiate(m_BulletProjectile, origin, Quaternion.LookRotation(originForward, Vector3.up));
            bullet.SetSpeed(m_Speed);
            
            return bullet;
        }
    }
}