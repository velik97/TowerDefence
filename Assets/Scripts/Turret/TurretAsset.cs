using Turret.Weapon;
using UnityEngine;

namespace Turret
{
    [CreateAssetMenu(menuName = "Assets/Turret Asset", fileName = "Turret Asset")]
    public class TurretAsset : ScriptableObject
    {
        public string TurretName;
        public Sprite TurretImage;

        public TurretView ViewPrefab;
        public TurretWeaponAssetBase WeaponAsset;

        public int Price;
    }
}