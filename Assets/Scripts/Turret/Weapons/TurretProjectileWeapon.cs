namespace Turret.Weapons
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