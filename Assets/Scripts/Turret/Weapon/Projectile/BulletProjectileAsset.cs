using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    [CreateAssetMenu(menuName = "Assets/Bullet Projectile Asset", fileName = "Bullet Projectile Asset")]
    public class BulletProjectileAsset : ProjectileAssetBase
    {
        [SerializeField]
        private BulletProjectile m_BulletProjectile;
        
        public override IProjectile CreateProjectile(Vector3 origin, Vector3 originForward, EnemyData enemyData)
        {
            return Instantiate(m_BulletProjectile, origin, Quaternion.LookRotation(originForward, Vector3.up));
        }
    }
}