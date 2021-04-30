using System;
using Runtime;
using Turret;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class TurretMarketUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_TurretMarketObject;

        [SerializeField]
        private RectTransform m_TurretsListHolder;

        [SerializeField]
        private Button m_StartBuildingButton;

        [SerializeField]
        private Button m_StopBuildingButton;

        [SerializeField]
        private TurretInfoUI m_TurretInfoUIPrefab;

        private void Awake()
        {
            SubscribeButtons();
            CreateTurretInfos();
            StopBuilding();
        }

        private void SubscribeButtons()
        {
            m_StartBuildingButton.onClick.AddListener(StartBuilding);
            m_StopBuildingButton.onClick.AddListener(StopBuilding);
        }

        private void CreateTurretInfos()
        {
            foreach (TurretAsset turretAsset in Game.CurrentLevel.TurretMarketAsset.TurretAssets)
            {
                TurretInfoUI turretInfoUI = Instantiate(m_TurretInfoUIPrefab, m_TurretsListHolder);
                turretInfoUI.SetAsset(turretAsset);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Game.Player.TurretMarket.IsBuilding)
                {
                    StopBuilding();
                }
                else
                {
                    StartBuilding();
                }
            }
        }

        private void StartBuilding()
        {
            Game.Player.TurretMarket.StartBuilding();
            m_TurretMarketObject.SetActive(true);
            m_StartBuildingButton.gameObject.SetActive(false);
        }

        private void StopBuilding()
        {
            Game.Player.TurretMarket.StopBuilding();
            m_TurretMarketObject.SetActive(false);
            m_StartBuildingButton.gameObject.SetActive(true);
        }
    }
}