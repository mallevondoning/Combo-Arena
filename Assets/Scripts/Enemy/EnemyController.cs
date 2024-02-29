using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    private float _mHealth;


    private void Awake()
    {
        _mHealth = _health;
    }

    private void Update()
    {
        _health = Mathf.Clamp(_health, 0,_mHealth);
    }

    public float GetHealth()
    {
        return _health;
    }
    public float GetMaxHealth()
    {
        return _mHealth;
    }
    public bool IsDead()
    {
        return _health < 0;
    }
}
