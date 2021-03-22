using UnityEngine;

namespace Turret.Weapons.Projectiles
{
    [CreateAssetMenu(menuName = "Assets/Turret Projectile Weapon Asset", fileName = "Turret Projectile Weapon Asset")]
    public class TurretProjectileWeaponAsset : TurretWeaponAssetBase
    {
        public ProjectileAssetBase ProjectileAsset;
        
        public float RateOfFire;
        public float MaxShotDistance;

        public override ITurretWeapon GetWeapon(TurretView view)
        {
            return new TurretProjectileWeapon(this, view);
        }
    }
}