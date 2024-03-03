using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Projectile _projectileData;
    [SerializeField] private Rigidbody _rb;

    private void Awake()
    {
        _projectileData.Init();

        _projectileData.CreateBullet.Invoke();
        _rb.useGravity = _projectileData.UseGravity;
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.magnitude <= 0) _rb.velocity = Velocity(_projectileData.Speed);
    }

    private Vector3 Velocity(float speed)
    {
        return transform.forward * speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        int hitGroundEvent = LayerMask.NameToLayer("Ground");
        int collisionMask = collision.gameObject.layer;

        EnemyController hitEnemyEvent = collision.gameObject.GetComponent<EnemyController>();
        ProjectileController hitBulletEvent = collision.gameObject.GetComponent<ProjectileController>();

        bool destroyCheck = false;
        if (hitGroundEvent == collisionMask)
        {
            _projectileData.HitGround.ModfilerEvent.Invoke(); 
            destroyCheck = _projectileData.HitGround.DestroyOnContact;
        }
        else if (hitEnemyEvent != null)
        {
            _projectileData.HitEnemy.ModfilerEvent.Invoke();
            destroyCheck = _projectileData.HitEnemy.DestroyOnContact;
        }
        else if (hitBulletEvent != null)
        {
            _projectileData.HitBullet.ModfilerEvent.Invoke();
            destroyCheck = _projectileData.HitBullet.DestroyOnContact;
        }

        if (destroyCheck) Destroy(gameObject); /*<--- Give more context*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Current projectile collider is not a trigger");
    }
}
