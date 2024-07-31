using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Projectile _projectileData;
    [SerializeField] private Rigidbody _rb;

    private Vector3 _startPos;

    private void Awake()
    {
        _projectileData.Init();

        _projectileData.CreateBullet.OnHit();
        _rb.useGravity = _projectileData.UseGravity;
       
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.magnitude <= 0) _rb.velocity = Velocity(_projectileData.Speed);
        SelfDestroy();
    }

    private Vector3 Velocity(float speed)
    {
        return transform.forward * speed;
    }

    private void SelfDestroy() // <--- update for more rules
    {
        if (Vector3.Distance(_startPos, transform.position) > 100f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        int hitGroundEvent = LayerMask.NameToLayer("Ground");
        int collisionMask = collision.gameObject.layer;

        EnemyController hitEnemyEvent = collision.gameObject.GetComponent<EnemyController>();
        ProjectileController hitBulletEvent = collision.gameObject.GetComponent<ProjectileController>();

        _projectileData.Hit(transform.position);

        bool destroyCheck = false;
        if (hitGroundEvent == collisionMask)
        {
            _projectileData.HitGround.ModfilerEvent.OnHit();
            destroyCheck = _projectileData.HitGround.DestroyOnContact;
        }
        else if (hitEnemyEvent != null)
        {
            _projectileData.HitEnemy.ModfilerEvent.OnHit();
            destroyCheck = _projectileData.HitEnemy.DestroyOnContact;
        }
        else if (hitBulletEvent != null)
        {
            _projectileData.HitBullet.ModfilerEvent.OnHit();
            destroyCheck = _projectileData.HitBullet.DestroyOnContact;
        }

        if (destroyCheck) Destroy(gameObject); /*<--- Give more context*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning("Current projectile collider is not a trigger");
    }
}
