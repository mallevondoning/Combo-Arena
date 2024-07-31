using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _enemyRigibody;
    [SerializeField] private BulletTeam _bulletTeam;

    [Header("Head")]
    [SerializeField] private Transform _headTrans;
    [SerializeField] private float _lookDist = 15;

    [Header("Status")]
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _enemyStatus;
    [SerializeField] private float _health = 100;
    private float _mHealth;

    private bool _newState = true;
    private StateMachine _brain = new StateMachine();

    private void Awake()
    {
        _mHealth = _health;
    }

    private void Update()
    {
        _health = Mathf.Clamp(_health, 0,_mHealth);
        _healthBar.fillAmount = _health / _mHealth;

        if (Vector3.Distance(transform.position,GameManager.Instance.Player.transform.position) < _lookDist) LookAtPlayer(); //update so distance scales where you stand
        else LookAtNeutral();

        Brain();
        //if (IsDead()) Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        //_enemyRigibody.velocity;
    }

    public void AddKnockback(float knockbackForce, Vector3 hitPos, float range)
    {
        _enemyRigibody.AddExplosionForce(knockbackForce, hitPos, range);
    }

    private void LookAtPlayer()
    {
        //add clamps
        _headTrans.LookAt(GameManager.Instance.Player.transform,Vector3.up);
    }
    private void LookAtNeutral()
    {
        _headTrans.LookAt(transform.forward, Vector3.up);
    }

    private void Brain()
    {
        if (_newState)
        {
            _brain.Enter();
            _newState = false;
        }

        _brain.Action();

        StateMachine _newBrain = _brain.Exit();
        if (!_newBrain.Equals(_brain))
        {
            _brain = _newBrain;
            _newState = true;
        }
    }

    public float GetHealth()
    {
        return _health;
    }
    public float GetMaxHealth()
    {
        return _mHealth;
    }
    public void TakeDamage(float value)
    {
        _health -= value;
    }
    public void Heal(float value)
    {
        _health += value;
    }
    public bool IsDead()
    {
        return _health <= 0;
    }
    public BulletTeam GetBulletTeam()
    {
        return _bulletTeam;
    }
}
