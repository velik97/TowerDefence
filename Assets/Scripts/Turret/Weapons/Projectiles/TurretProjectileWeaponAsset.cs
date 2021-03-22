namespace Turret.Weapons.Projectiles
{
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