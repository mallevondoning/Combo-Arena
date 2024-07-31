using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/World/Projectile")]
public class Projectile : ScriptableObject
{
    [Header("Enum")]
    public List<ElementType> InflictElementList = new List<ElementType>();
    public BulletTeam BulletTeam = BulletTeam.NoneID;

    [Header("Effects")]
    public ProjectileEffects CreateBullet = new ProjectileEffects();
    public ProjectileModifier HitGround = new ProjectileModifier();
    public ProjectileModifier HitEnemy = new ProjectileModifier();
    public ProjectileModifier HitBullet = new ProjectileModifier();

    [Header("Basic")]
    public float Speed = 15;
    public bool UseGravity = false;

    private Vector3 _hitPos;

    public void Init() { }

    public void Hit(Vector3 hitPos)
    {
        HitGround.ModfilerEvent.Init(_hitPos, BulletTeam);
        HitEnemy.ModfilerEvent.Init(_hitPos, BulletTeam);
        HitBullet.ModfilerEvent.Init(_hitPos, BulletTeam);
    }
}
