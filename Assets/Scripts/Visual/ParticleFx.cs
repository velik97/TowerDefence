using System;
using UnityEngine;

namespace Visual
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleFx : MonoBehaviour
    {
        private void Awake()
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            ps.Play();
        }
    }
}