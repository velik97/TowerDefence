using System;
using System.Collections.Generic;
using Enemy;
using Runtime;
using UnityEngine;
using Utils.Pooling;

namespace UI.InGame.Overtips
{
    public class OvertipManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyOvertip m_EnemyOvertipPrefab;
        
        private Dictionary<EnemyData, EnemyOvertip> m_OvertipsDictionary = new Dictionary<EnemyData, EnemyOvertip>();

        private void OnEnable()
        {
            Game.Player.OnEnemySpawned += EnemySpawned;
            Game.Player.OnEnemyDied += EnemyDied;
        }

        private void OnDisable()
        {
            Game.Player.OnEnemySpawned -= EnemySpawned;
            Game.Player.OnEnemyDied -= EnemyDied;
        }

        private void EnemySpawned(EnemyData enemyData)
        {
            EnemyOvertip overtip = GameObjectPool.InstantiatePooled(m_EnemyOvertipPrefab, transform);
            overtip.SetEnemyData(enemyData);

            m_OvertipsDictionary.Add(enemyData, overtip);
        }

        private void EnemyDied(EnemyData enemyData)
        {
            EnemyOvertip overtip = m_OvertipsDictionary[enemyData];
            m_OvertipsDictionary.Remove(enemyData);

            GameObjectPool.ReturnObjectToPool(overtip);
        }
    }
}