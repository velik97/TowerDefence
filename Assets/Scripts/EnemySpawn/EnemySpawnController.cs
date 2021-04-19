using System.Collections;
using Enemy;
using Runtime;
using UnityEngine;
using Utils.Pooling;
using Grid = Field.Grid;

namespace EnemySpawn
{
    public class EnemySpawnController : IController
    {
        private SpawnWavesAsset m_SpawnWaves;
        private Grid m_Grid;

        private IEnumerator m_SpawnEnumerator;

        private float m_WaitForSecondsEndTime;

        public EnemySpawnController(SpawnWavesAsset spawnWaves, Grid grid)
        {
            m_SpawnWaves = spawnWaves;
            m_Grid = grid;
        }

        public void OnStart()
        {
            m_SpawnEnumerator = SpawnEnumerator();
            m_WaitForSecondsEndTime = Time.time;
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            if (m_WaitForSecondsEndTime > Time.time)
            {
                return;
            }
            
            if (m_SpawnEnumerator.MoveNext())
            {
                if (m_SpawnEnumerator.Current is CustomWaitForSeconds waitForSeconds)
                {
                    m_WaitForSecondsEndTime = Time.time + waitForSeconds.Seconds;
                }
            }
        }

        private IEnumerator SpawnEnumerator()
        {
            int waveNum = 0;
            foreach (SpawnWave wave in m_SpawnWaves.SpawnWaves)
            {
                Game.Player.SetWave(waveNum++);
                yield return new CustomWaitForSeconds(wave.TimeBeforeStartWave);
                for (int i = 0; i < wave.Count; i++)
                {
                    SpawnEnemy(wave.EnemyAsset);
                    yield return new CustomWaitForSeconds(wave.TimeBetweenSpawns);
                }
            }
            
            Game.Player.LastWaveSpawned();
        }

        private void SpawnEnemy(EnemyAsset asset)
        {
            EnemyView view = GameObjectPool.InstantiatePooled(asset.ViewPrefab);
            view.transform.position = m_Grid.GetStartNode().Position;
            EnemyData data = new EnemyData(asset);

            data.AttachView(view);
            view.CreateMovementAgent(m_Grid);

            Game.Player.EnemySpawned(data);
        }

        private class CustomWaitForSeconds
        {
            public readonly float Seconds;

            public CustomWaitForSeconds(float seconds)
            {
                Seconds = seconds;
            }
        }
    }
}