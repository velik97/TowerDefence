using UnityEngine;

namespace Turret.Weapons.Projectiles.Bullet
{
    public class BulletProjectile : ProjectileBase<BulletProjectileAsset>
    {
        protected override void TickApproaching()
        {
            Vector3 delta = EnemyData.View.transform.position - transform.position;
            delta = delta.normalized * Asset.Speed;

            transform.Translate(delta);
        }

        protected override void OnCollidedWithEnemy()
        {
            EnemyData.GetDamage(Asset.Damage);
        }
    }
}