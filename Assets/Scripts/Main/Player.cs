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

        private bool m_AllWavesAreSpawned = false;
        
        private int m_Health;
        public int Health => m_Health;
        public event Action<int> HealthChanged;

        private int m_WaveNumber = 1;
        public int WaveNumber => m_WaveNumber;
        public event Action<int> WaveNumberChanged; 

        public Player()
        {
            GridHolder = Object.FindObjectOfType<GridHolder>();
            GridHolder.CreateGrid();
            Grid = GridHolder.Grid;

            TurretMarket = new TurretMarket();

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

        public void EnemyReachedTarget(EnemyData data)
        {
            m_EnemyDatas.Remove(data);
        }

        public void LastWaveSpawned()
        {
            m_AllWavesAreSpawned = true;
        }

        public void ApplyDamage(int damage)
        {
            m_Health -= damage;
            if (m_Health < 0)
            {
                m_Health = 0;
            }
            HealthChanged?.Invoke(m_Health);
        }

        public void SetWaveNumber(int waveNumber)
        {
            m_WaveNumber = waveNumber;
            WaveNumberChanged?.Invoke(waveNumber);
        }

        public void TurretSpawned(TurretData data)
        {
            m_TurretDatas.Add(data);
        }
        
        public void CheckForWin()
        {
            if (m_AllWavesAreSpawned && m_EnemyDatas.Count == 0)
            {
                GameWon();
            }
        }

        private void GameWon()
        {
            Game.StopPlayer();
            Debug.Log("Win!");
        }

        public void CheckForLose()
        {
            if (m_Health <= 0)
            {
                GameLost();
            }
        }

        private void GameLost()
        {
            Game.StopPlayer();
            Debug.Log("Lose!");
        }
    }
}