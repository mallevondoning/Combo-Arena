using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private Image _healthBarImg;
    [SerializeField] private Gradient _healthColor;

    private void Update()
    {
        _healthBarImg.fillAmount = GameManager.Instance.PlayerStatus.NormHealth();
        _healthBarImg.color = _healthColor.Evaluate(GameManager.Instance.PlayerStatus.NormHealth());
    }
}
