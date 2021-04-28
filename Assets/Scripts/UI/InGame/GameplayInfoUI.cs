using System;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class GameplayInfoUI : MonoBehaviour
    {
        [SerializeField]
        private Text m_HeathText;
        
        [SerializeField]
        private Text m_MoneyText;

        [SerializeField]
        private Text m_WaveText;

        private void OnEnable()
        {
            SetHealth(Game.Player.Health);
            SetMoney(Game.Player.TurretMarket.Money);
            SetWave(Game.Player.WaveNumber);
            
            Game.Player.HealthChanged += SetHealth;
            Game.Player.TurretMarket.MoneyChanged += SetMoney;
            Game.Player.WaveNumberChanged += SetWave;
        }

        private void OnDisable()
        {
            Game.Player.HealthChanged -= SetHealth;
            Game.Player.TurretMarket.MoneyChanged -= SetMoney;
            Game.Player.WaveNumberChanged -= SetWave;
        }

        private void SetHealth(int health)
        {
            m_HeathText.text = $"Health: {health}";
        }

        private void SetMoney(int money)
        {
            m_MoneyText.text = $"Money: {money}";
        }

        private void SetWave(int wave)
        {
            m_WaveText.text = $"Wave: {wave}";
        }
    }
}