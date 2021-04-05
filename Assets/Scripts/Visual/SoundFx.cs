using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Visual
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundFx : MonoBehaviour
    {
        [SerializeField]
        private AudioClip m_AudioClip;

        [SerializeField]
        private float m_MinPitch;
        [SerializeField]
        private float m_MaxPitch;

        [SerializeField]
        private float m_Volume;
        
        private void Awake()
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = m_AudioClip;
            audioSource.pitch = Random.Range(m_MinPitch, m_MaxPitch);
            audioSource.volume = m_Volume;

            audioSource.Play();
        }
    }
}