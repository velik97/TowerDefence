using UnityEngine;

namespace Turret.Weapons
{
    public abstract class TurretWeaponAssetBase : ScriptableObject
    {
        public abstract ITurretWeapon GetWeapon(TurretView view);
    }
}