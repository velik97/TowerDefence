using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI
{
    public class GameplayInfoUI : MonoBehaviour
    {
        [SerializeField]
        private Text m_HealthText;

        [SerializeField]
        private Text m_MoneyText;

        [SerializeField]
        private Text m_WavesText;

        private int m_StartHealth;
        private int m_WavesCount;

        private void Awake()
        {
            m_StartHealth = Game.CurrentLevel.StartHealth;
            m_WavesCount = Game.CurrentLevel.SpawnWavesAsset.SpawnWaves.Length;
            
            SetHealth(Game.Player.Health);
            SetMoney(Game.Player.TurretMarket.Money);
            SetWave(Game.Player.CurrentWave);
        }

        private void OnEnable()
        {
            Game.Player.HealthChanged += SetHealth;
            Game.Player.TurretMarket.MoneyChanged += SetMoney;
            Game.Player.CurrentWaveChanged += SetWave;
        }

        private void OnDisable()
        {
            Game.Player.HealthChanged -= SetHealth;
            Game.Player.TurretMarket.MoneyChanged -= SetMoney;
            Game.Player.CurrentWaveChanged -= SetWave;
        }

        private void SetHealth(int health)
        {
            m_HealthText.text = $"Health: {health}/{m_StartHealth}";
        }

        private void SetMoney(int money)
        {
            m_MoneyText.text = $"Money: {money}";
        }

        private void SetWave(int wave)
        {
            m_WavesText.text = $"Wave: {wave + 1}/{m_WavesCount}";
        }
    }
}