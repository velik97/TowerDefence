using System;
using Enemy;
using Runtime;
using Turret;
using UnityEngine;

namespace TurretSpawn
{
    public class TurretMarket
    {
        private TurretMarketAsset m_Asset;

        private TurretAsset m_SelectedTurret;
        
        private int m_Money;
        public int Money => m_Money;
        public event Action<int> MoneyChanged;

        public TurretMarket(TurretMarketAsset asset)
        {
            m_Asset = asset;
            m_Money = Game.CurrentLevel.StartMoney;
        }

        public TurretAsset SelectedTurret
        {
            get
            {
                if (m_SelectedTurret == null)
                {
                    return null;
                }

                if (m_Money >= m_SelectedTurret.Price)
                {
                    return m_SelectedTurret;
                }

                return null;
            }
        }

        public void BuyTurret(TurretAsset turretAsset)
        {
            if (turretAsset.Price > m_Money)
            {
                Debug.LogError("Not enough money!");
                return;
            }
            m_Money -= turretAsset.Price;
            MoneyChanged?.Invoke(m_Money);
        }

        public void SelectTurret(TurretAsset turretAsset)
        {
            m_SelectedTurret = turretAsset;
        }

        public void GetReward(EnemyData enemyData)
        {
            m_Money += enemyData.Asset.Reward;
            MoneyChanged?.Invoke(m_Money);
        }
    }
}