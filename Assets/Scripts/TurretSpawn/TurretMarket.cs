using System;
using Enemy;
using Runtime;
using Turret;

namespace TurretSpawn
{
    public class TurretMarket
    {
        private TurretMarketAsset m_Asset;
        private int m_Money;
        public int Money => m_Money;
        public event Action<int> MoneyChanged; 

        public TurretMarket(TurretMarketAsset asset)
        {
            m_Asset = asset;
            m_Money = Game.CurrentLevel.StartMoney;
            MoneyChanged?.Invoke(m_Money);
        }

        public void BuyTurret(TurretAsset asset)
        {
            m_Money -= asset.Price;
            MoneyChanged?.Invoke(m_Money);
        }

        public TurretAsset ChosenTurret
            => m_Money < m_Asset.TurretAssets[0].Price
                ? null
                : m_Asset.TurretAssets[0];

        public void GiveReward(EnemyData enemyData)
        {
            m_Money += enemyData.Asset.Reward;
            MoneyChanged?.Invoke(m_Money);
        }
    }
}