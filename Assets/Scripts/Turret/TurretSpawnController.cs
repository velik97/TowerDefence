using Field;
using Runtime;
using Turret.Market;
using UnityEngine;
using Grid = Field.Grid;

namespace Turret
{
    public class TurretSpawnController : IController
    {
        private TurretMarket m_Market;
        private Grid m_Grid;

        public TurretSpawnController(TurretMarket market, Grid grid)
        {
            m_Market = market;
            m_Grid = grid;
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            if (m_Market.ChosenAsset == null || !m_Grid.HasSelectedNode())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                SpawnTurret(m_Grid.GetSelectedNode(), m_Market.ChosenAsset);
            }
        }

        private void SpawnTurret(Node node, TurretAsset turretAsset)
        {
            TurretData data = new TurretData(turretAsset, node);
            TurretView view = Object.Instantiate(turretAsset.ViewPrefab);
            node.IsOccupied = true;

            view.transform.position = node.Position;

            data.AttachView(view);

            Game.Player.TurretSpawned(data);
        }
    }
}