using UnityEngine;

namespace Turret.Market
{
    public class TurretMarket
    {
        private TurretMarketAsset m_MarketAsset;
        
        public TurretAsset ChosenAsset => m_MarketAsset.Turrets[0];

        public TurretMarket(TurretMarketAsset marketAsset)
        {
            m_MarketAsset = marketAsset;
        }

    }
}