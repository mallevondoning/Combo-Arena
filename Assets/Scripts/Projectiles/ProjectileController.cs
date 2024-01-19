using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Projectile _projectileData;
    [SerializeField] private Rigidbody _rb;

    private Transform[] _sizeObject = new Transform[3];
    private Vector3 _dir;

    public void Init(Vector3 dir)
    {
        _dir = dir;
    }

    private void Awake()
    {
        _projectileData.CreateBullet.Invoke();

        for (int i = 0; i < transform.childCount; i++) _sizeObject[i] = transform.GetChild(i);

        switch (_projectileData.ProjectileSize)
        {
            case ProjectileSize.Small:
                _sizeObject[0].gameObject.SetActive(true);
                break;
            case ProjectileSize.Medium:
                _sizeObject[1].gameObject.SetActive(true);
                break;
            case ProjectileSize.Large:
                _sizeObject[2].gameObject.SetActive(true);
                break;
            case ProjectileSize.NoneID:
                Debug.Log("No Size Set");
                break;
        }

        _projectileData.Init(this);

        _rb.useGravity = _projectileData.UseGravity;

        //_dir = Vector3.forward; //<--- remove
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.magnitude <= 0) _rb.velocity = Velocity(_dir, _projectileData);
    }

    private Vector3 Velocity( Vector3 dir, Projectile projectileData)
    {
        return dir.normalized * projectileData.Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int hitGroundEvent = LayerMask.NameToLayer("Ground");

        //<update> could change to empty script
        EnemyController hitEnemyEvent = collision.gameObject.GetComponent<EnemyController>();
        ProjectileController hitBulletEvent = collision.gameObject.GetComponent<ProjectileController>();
        //</update>

        int collisionMask = collision.gameObject.layer;

        if (hitGroundEvent == collisionMask) _projectileData.HitGround.Invoke();
        else if (hitEnemyEvent != null) _projectileData.HitEnemy.Invoke();
        else if (hitBulletEvent != null) _projectileData.HitBullet.Invoke();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
