using System;
using Turret.Weapons.Projectiles;

namespace Turret.Weapons
{
    public static class TurretWeaponFactory
    {
        public static ITurretWeapon GetWeapon(TurretWeaponAssetBase turretWeaponAsset, TurretView view)
        {
            switch (turretWeaponAsset)
            {
                case TurretProjectileWeaponAsset turretProjectileWeaponAsset:
                    return new TurretProjectileWeapon(turretProjectileWeaponAsset, view);
                default:
                    throw new ArgumentOutOfRangeException(nameof(turretWeaponAsset));
            }
        }
    }
}