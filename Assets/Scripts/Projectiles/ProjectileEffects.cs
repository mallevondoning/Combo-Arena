using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectileEffects
{
    [SerializeReference, SerializeReferenceButton] private IProjectileEffets[] _effets;

    private Vector3 _hitPos;
    private BulletTeam _bulletTeam;

    public void Init(Vector3 hitPos, BulletTeam bulletTeam)
    {
        _hitPos = hitPos;
        _bulletTeam = bulletTeam;
    }

    public void OnHit()
    {
        for (int i = 0; i < _effets.Length; i++)
        {
            if (_effets[i] != null) continue;

            _effets[i].Init(_hitPos, _bulletTeam);

            _effets[i].Action();
        }
    }
}
