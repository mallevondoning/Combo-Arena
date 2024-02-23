using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Game/World/Projectile")]
public class Projectile : ScriptableObject
{
    [Header("Enum")]
    public List<ElementType> InflictElementList = new List<ElementType>();
    public ProjectileSize ProjectileSize = ProjectileSize.NoneID;
    public BulletTeam BulletTeam = BulletTeam.NoneID;

    [Header("UnityEvents")] //<-- change to a tag system
    public UnityEvent CreateBullet = new UnityEvent();
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

    private ProjectileController _projectileCtrl;

    public void Init(ProjectileController projectileCtrl)
    {
        CreateBullet.AddListener(CreateBulletFunc);
        HitGround.ModfilerEvent.AddListener(HitGroundFunc);
        HitEnemy.ModfilerEvent.AddListener(HitEnemyFunc);
        HitBullet.ModfilerEvent.AddListener(HitBulletFunc);

        _projectileCtrl = projectileCtrl;
    }

    protected virtual void CreateBulletFunc()
    {
    }
    protected virtual void HitGroundFunc()
    {
    }
    protected virtual void HitEnemyFunc()
    {
    }
    protected virtual void HitBulletFunc()
    {
    }
}
