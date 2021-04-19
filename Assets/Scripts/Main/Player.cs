using System;
using System.Collections.Generic;
using Enemy;
using Field;
using Runtime;
using Turret;
using Turret.Weapon;
using TurretSpawn;
using UnityEngine;
using Grid = Field.Grid;
using Object = UnityEngine.Object;

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

        private bool m_AllWavesAreSpawned;

        public bool AllWavesAreSpawned => m_AllWavesAreSpawned;
        
        private int m_Health;
        public int Health => m_Health;
        public event Action<int> HealthChanged;

        private int m_CurrentWave;
        public int CurrentWave => m_CurrentWave;
        public event Action<int> CurrentWaveChanged;

        public event Action LostGame;
        public event Action WonGame;
        
        public Player()
        {
            GridHolder = Object.FindObjectOfType<GridHolder>();
            GridHolder.CreateGrid();
            Grid = GridHolder.Grid;

            TurretMarket = new TurretMarket(Game.CurrentLevel.TurretMarketAsset);

            EnemySearch = new EnemySearch(m_EnemyDatas);

            m_Health = Game.CurrentLevel.StartHealth;
            HealthChanged?.Invoke(m_Health);
            m_AllWavesAreSpawned = false;

            m_CurrentWave = 0;
            CurrentWaveChanged?.Invoke(m_CurrentWave);
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
            HealthChanged?.Invoke(m_Health);
        }

        public void SetWave(int wave)
        {
            m_CurrentWave = wave;
            CurrentWaveChanged?.Invoke(wave);
        }
        
        public void CheckForGameOver()
        {
            if (m_Health <= 0)
            {
                GameLost();
            }
        }

        private void GameLost()
        {
            Game.StopRunner();
            LostGame?.Invoke();
        }
        
        public void LastWaveSpawned()
        {
            m_AllWavesAreSpawned = true;
        }

        public void GameWon()
        {
            Game.StopRunner();
            WonGame?.Invoke();
        }

        public void TurretSpawned(TurretData data)
        {
            m_TurretDatas.Add(data);
        }
    }
}