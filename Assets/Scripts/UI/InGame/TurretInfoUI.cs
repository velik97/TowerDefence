using Runtime;
using Turret;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class TurretInfoUI : MonoBehaviour
    {
        private TurretAsset m_TurretAsset;
        
        [SerializeField]
        private Text m_TurretName;

        [SerializeField]
        private Text m_TurretDescription;

        [SerializeField]
        private Text m_TurretPrice;

        [SerializeField]
        private Image m_TurretImage;

        [SerializeField]
        private Button m_SelectButton;

        [SerializeField]
        private CanvasGroup m_CanvasGroup;
        
        public void SetAsset(TurretAsset asset)
        {
            m_TurretAsset = asset;
            
            m_TurretName.text = asset.TurretName;
            m_TurretDescription.text = asset.WeaponAsset.GetDescription();
            m_TurretPrice.text = $"Price: {asset.Price}";
            m_TurretImage.sprite = asset.TurretImage;

            
            m_SelectButton.onClick.RemoveAllListeners();
            m_SelectButton.onClick.AddListener(SelectTurret);
            
            RefreshAvailability(Game.Player.TurretMarket.Money);
        }

        private void OnEnable()
        {
            Game.Player.TurretMarket.MoneyChanged += RefreshAvailability;
        }
        
        private void OnDisable()
        {
            Game.Player.TurretMarket.MoneyChanged -= RefreshAvailability;
        }

        private void RefreshAvailability(int money)
        {
            bool canBuy = m_TurretAsset != null && m_TurretAsset.Price <= money;
            
            m_SelectButton.interactable = canBuy;
            m_CanvasGroup.alpha = canBuy ? 1f : .5f;
        }

        private void SelectTurret()
        {
            Game.Player.TurretMarket.SelectTurret(m_TurretAsset);
        }
    }
}