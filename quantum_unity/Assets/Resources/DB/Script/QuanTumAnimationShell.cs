using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;

public class QuanTumAnimationShell : QuantumCallbacks
{
    [SerializeField]
    private ParticleSystem m_ExplosionAnimation;

    private void OnDestroy()
    {
        m_ExplosionAnimation.transform.parent = null;
        m_ExplosionAnimation.Play();
        ParticleSystem.MainModule mainModule = m_ExplosionAnimation.main;
        Destroy(m_ExplosionAnimation.gameObject, mainModule.duration);
        Destroy(gameObject);
    }
}
