using Enemy;
using UnityEngine;

namespace Turret.Weapons.Projectiles.Bullet
{
    [CreateAssetMenu(menuName = "Assets/Bullet Projectile Asset", fileName = "Bullet Projectile Asset")]
    public class BulletProjectileAsset : ProjectileAssetBase
    {
        [SerializeField]
        private BulletProjectile m_Bullet;

        public int Damage;
        public float Speed;
        
        public override MonoBehaviour CreateProjectile(Vector3 origin, Vector3 originForward, EnemyData enemyData)
        {
            BulletProjectile bullet = Instantiate(m_Bullet);
            bullet.OnInstantiated(origin, originForward, enemyData, this);

            return bullet;
        }
    }
}