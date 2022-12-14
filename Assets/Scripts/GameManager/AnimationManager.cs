using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Collider _myCollider;
    ParticleSystem _myParticles;

    void Start()
    {
        _myCollider = GetComponentInParent<Collider>();
        _myParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void EnableCollider()
    {
        _myCollider.enabled = true;
    }
    public void DisableCollider()
    {
        _myCollider.enabled = false;
    }
    public void PlayParticles()
    {
        _myParticles.Play();
    }
    public void NextLevel() { GameManager.Instance.NextLevel(); }
}
