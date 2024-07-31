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
    public float Damage = 10;
    public float Speed = 15;
    public bool UseGravity = false;
    [Header("Knockback")]
    public bool HasKnockback = false;
    public float KnockbackForce = 0;

    public void Init() { }
}
