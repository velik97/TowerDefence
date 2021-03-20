using Field;
using Runtime;
using Turret;
using UnityEngine;
using Grid = Field.Grid;

namespace TurretSpawn
{
    public class TurretSpawnController : IController
    {
        private Grid m_Grid;
        private TurretMarket m_Market;

        public TurretSpawnController(Grid grid, TurretMarket market)
        {
            m_Grid = grid;
            m_Market = market;
        }

        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_Grid.HasSelectedNode() && m_Market.ChosenTurret != null)
                {
                    Node node = m_Grid.GetSelectedNode();
                    if (node.IsOccupied /* || !m_Grid.CanOccupy(node) */)
                    {
                        return;
                    }
                    SpawnTurret(m_Market.ChosenTurret, node);
                }
            }
        }

        private void SpawnTurret(TurretAsset asset, Node node)
        {
            TurretView view = Object.Instantiate(asset.ViewPrefab);
            TurretData data = new TurretData(asset, node);
            data.AttachView(view);

            node.IsOccupied = true;
        }
    }
}