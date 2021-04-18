using System.Collections.Generic;
using Enemy;
using Field;
using Runtime;
using Turret;
using Turret.Weapon;
using TurretSpawn;
using UnityEngine;
using Grid = Field.Grid;

namespace Main
{
    public class Player
    {
        private List<EnemyData> m_EnemyDatas = new List<EnemyData>();
        public IReadOnlyList<EnemyData> EnemyDatas => m_EnemyDatas;

        private List<TurretData> m_TurretDatas = new List<TurretData>();
        public IReadOnlyList<TurretData> TurretDatas => m_TurretDatas;

        public readonly GridHolder GridHolder;
        public readonly Grid Grid;
        public readonly TurretMarket TurretMarket;
        public readonly EnemySearch EnemySearch;

        private int m_Health;
        public int Health => m_Health;

        public Player()
        {
            GridHolder = Object.FindObjectOfType<GridHolder>();
            GridHolder.CreateGrid();
            Grid = GridHolder.Grid;

            TurretMarket = new TurretMarket(Game.CurrentLevel.TurretMarketAsset);

            EnemySearch = new EnemySearch(m_EnemyDatas);

            m_Health = Game.CurrentLevel.StartHealth;
        }

        public void EnemySpawned(EnemyData data)
        {
            m_EnemyDatas.Add(data);
        }
        
        public void EnemyDied(EnemyData data)
        {
            m_EnemyDatas.Remove(data);
        }

        public void EnemyReachedFortress(EnemyData data)
        {
            m_EnemyDatas.Remove(data);
            m_Health -= data.Asset.Damage;
        }

        public void CheckForGameOver()
        {
            if (m_Health <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            Game.StopRunner();
            Debug.Log("Game Over :(");
        }

        public void TurretSpawned(TurretData data)
        {
            m_TurretDatas.Add(data);
        }
    }
}