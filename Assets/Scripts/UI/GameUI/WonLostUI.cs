using System;
using Runtime;
using UnityEngine;

namespace UI.GameUI
{
    public class WonLostUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_WonTextObject;
        
        [SerializeField]
        private GameObject m_LostTextObject;

        private void Awake()
        {
            m_WonTextObject.SetActive(false);
            m_LostTextObject.SetActive(false);
        }

        private void OnEnable()
        {
            Game.Player.WonGame += ShowWonText;
            Game.Player.LostGame += ShowLostText;
        }
        
        private void OnDisable()
        {
            Game.Player.WonGame -= ShowWonText;
            Game.Player.LostGame -= ShowLostText;
        }

        private void ShowWonText()
        {
            m_WonTextObject.SetActive(true);
        }

        private void ShowLostText()
        {
            m_LostTextObject.SetActive(true);
        }
    }
}