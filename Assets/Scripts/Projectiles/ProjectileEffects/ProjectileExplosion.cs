using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectileExplosion : IProjectileEffets
{
    [SerializeField] private float _damage;
    [SerializeField] private bool _knockback;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _range;
    [SerializeReference, SerializeReferenceButton] private IEase _ease;

    private Vector3 _hitPos;
    private RaycastHit[] _hit;
    private BulletTeam _bulletTeam;

    public void Init(Vector3 hitPos, BulletTeam bulletTeam)
    {
        _hitPos = hitPos;
        _bulletTeam = bulletTeam;
    }

    public void Action()
    {
        _hit = Physics.SphereCastAll(_hitPos, _range, Vector3.up);

        for (int i = 0; i < _hit.Length; i++)
        {
            if (_hit[i].collider != null) continue;

            PlayerController playerController = _hit[i].collider.GetComponent<PlayerController>();
            if (playerController != null && _bulletTeam != BulletTeam.Frendly)
            {
                if (_knockback) playerController.AddKnockback(_knockbackForce, _hitPos, _range);

                float dist = Vector3.Distance(_hitPos, playerController.transform.position);
                playerController.TakeDamage(DamageCalc(dist));
                continue;
            }

            EnemyController enemyController = _hit[i].collider.GetComponent<EnemyController>();
            if (enemyController != null && enemyController.GetBulletTeam() != _bulletTeam)
            {
                if (_knockback) enemyController.AddKnockback(_knockbackForce, _hitPos, _range);

                float dist = Vector3.Distance(_hitPos, enemyController.transform.position);
                enemyController.TakeDamage(DamageCalc(dist));
                continue;
            }
        }
    }

    private float DamageCalc(float dist)
    {
        float t = _ease.Ease(dist, _range);
        float finalDamage = Mathf.Lerp(_damage, 1, t);
        return finalDamage;
    }
}
