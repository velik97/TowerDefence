using System;

namespace Turret.Weapons
{
    public static class TurretWeaponFactory
    {
        public static ITurretWeapon GetWeapon(TurretWeaponAssetBase turretWeaponAsset)
        {
            switch (turretWeaponAsset)
            {
                case TurretProjectileWeaponAsset turretProjectileWeaponAsset:
                    return new TurretProjectileWeapon(turretProjectileWeaponAsset);
                default:
                    throw new ArgumentOutOfRangeException(nameof(turretWeaponAsset));
            }
        }
    }
}