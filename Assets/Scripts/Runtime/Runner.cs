﻿using System;
using System.Collections.Generic;
using Enemy;
using Field;
using Spawn;
using Turret;
using UnityEngine;

namespace Runtime
{
    public class Runner : MonoBehaviour
    {
        private List<IController> m_Controllers;
        private bool m_IsRunning = false;

        private void Update()
        {
            if (!m_IsRunning)
            {
                return;
            }
            TickControllers();
        }

        public void StartRunning()
        {
            CreateAllControllers();
            OnStartControllers();
            m_IsRunning = true;
        }

        public void StopRunning()
        {
            OnStopControllers();
            m_IsRunning = false;
        }

        private void CreateAllControllers()
        {
            m_Controllers = new List<IController>
            {
                new GirdPointerController(Game.Player.GridHolder),
                new SpawnController(Game.CurrentLevel.SpawnWavesAsset, Game.Player.Grid),
                new TurretSpawnController(Game.Player.TurretMarket, Game.Player.Grid),
                new MovementController()
            };
        }

        private void OnStartControllers()
        {
            foreach (IController controller in m_Controllers)
            {
                try
                {
                    controller.OnStart();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        
        private void TickControllers()
        {
            foreach (IController controller in m_Controllers)
            {
                try
                {
                    controller.Tick();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        
        private void OnStopControllers()
        {
            foreach (IController controller in m_Controllers)
            {
                try
                {
                    controller.OnStop();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}