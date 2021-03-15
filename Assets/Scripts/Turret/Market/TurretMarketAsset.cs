using UnityEngine;

namespace Turret.Market
{
    [CreateAssetMenu(menuName = "Assets/Turret Market Asset", fileName = "Turret Market Asset")]
    public class TurretMarketAsset : ScriptableObject
    {
        public TurretAsset[] Turrets;
    }
}