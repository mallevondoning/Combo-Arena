using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;
    [Header("Health bar")]
    [SerializeField] private Image _healthBarImg;
    [Header("Status element")]
    [SerializeField] private Image _fireBarImg;
    [SerializeField] private Image _lightingBarImg;
    [SerializeField] private Image _iceBarImg;

    private void Update()
    {
        _healthBarImg.fillAmount = _enemy.GetHealth() / _enemy.GetMaxHealth();


    }
}
