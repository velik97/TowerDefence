namespace Turret.Weapons.Projectiles
{
    public class TurretProjectileWeapon : ITurretWeapon
    {
        private TurretProjectileWeaponAsset m_Asset;

        public TurretProjectileWeapon(TurretProjectileWeaponAsset asset)
        {
            m_Asset = asset;
        }

        public void TickShoot()
        {
        }
    }
}