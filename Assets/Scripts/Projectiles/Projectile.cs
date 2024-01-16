using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/World/Projectile")]
public class Projectile : ScriptableObject
{
    [Header("Enum")]
    public ElementType ElementType = ElementType.NoneID;
    public ProjectileSize ProjectileSize = ProjectileSize.NoneID;
    public BulletTeam BulletTeam = BulletTeam.NoneID;
    
    [Header("UnityEvents")]
    public UnityEvent HitGround = new UnityEvent();
    public UnityEvent HitEnemy = new UnityEvent();
    public UnityEvent HitBullet = new UnityEvent();

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
        HitGround.AddListener(HitGroundFunc);
        HitEnemy.AddListener(HitEnemyFunc);
        HitBullet.AddListener(HitBulletFunc);

        _projectileCtrl = projectileCtrl;
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

    //<bug> only delets the newest bullet
    public void DestroyBullet()
    {
        if (_projectileCtrl != null) _projectileCtrl.DestroySelf();
    }
    //</bug>
}
