using Runtime;
using Turret;

namespace TurretSpawn
{
    public class TurretMarket
    {
        private TurretMarketAsset m_Asset;
        private int m_Money;

        public TurretMarket(TurretMarketAsset asset)
        {
            m_Asset = asset;
            m_Money = Game.CurrentLevel.StartMoney;
        }

        public void BuyTurret(TurretAsset asset)
        {
            m_Money -= asset.Price;
        }

        public TurretAsset ChosenTurret
            => m_Money < m_Asset.TurretAssets[0].Price
                ? null
                : m_Asset.TurretAssets[0];
    }
}