using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBehavior : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CanvasGroup _cg;
    [SerializeField] private Rigidbody2D _rb;

    [Header("Status components")]
    [SerializeField] private StatusElementType _statusType;
    [SerializeField] private Gradient _statusGradient;

    [Header("Varibles")]
    [SerializeField][Range(0, 360)] private float _moveAngleInDegrees;
    [SerializeField] private float _maxMoveSpeed = 30;
    [SerializeField][Range(0, 1)] private float _shakeThershold = 0.9f;
    [SerializeField] private float _maxShakeSpeed = 25;
    [SerializeField] private float _maxShakeMagnitude = 35;

    private float _normAlpha;
    private float _normShake;

    private void Update()
    {
        FindElement(_statusType);

        _cg.alpha = _statusGradient.Evaluate(_normAlpha).a; //<== double check
    }

    private void FixedUpdate()
    {
        if (_cg.alpha > _shakeThershold) Shake();
        //else Move();
    }

    private void FindElement(StatusElementType statusType)
    {
        switch (statusType)
        {
            case StatusElementType.Ablaze:
                _normAlpha = GameManager.Instance.PlayerStatus.NormAblaze(_shakeThershold, true);
                _normShake = GameManager.Instance.PlayerStatus.NormAblaze(_shakeThershold, false);
                break;
            case StatusElementType.Shocked:
                _normAlpha = GameManager.Instance.PlayerStatus.NormShocked(_shakeThershold, true);
                _normShake = GameManager.Instance.PlayerStatus.NormShocked(_shakeThershold, false);
                break;
            case StatusElementType.Frozen:
                _normAlpha = GameManager.Instance.PlayerStatus.NormFrozen(_shakeThershold, true);
                _normShake = GameManager.Instance.PlayerStatus.NormFrozen(_shakeThershold, false);
                break;
        }
    }

    private void Move()
    {
        Vector3 dir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * _moveAngleInDegrees), Mathf.Sin(Mathf.Deg2Rad * _moveAngleInDegrees),0);
        float moveSpeed = Mathf.Lerp(_maxMoveSpeed, 0, _normAlpha);

        _rb.velocity = dir * moveSpeed;
    }
    private void Shake()
    {
        float perlinX = Mathf.PerlinNoise1D(Time.time);
        float perlinY = Mathf.PerlinNoise1D(Time.time + 1);

        float shakeSpeed = Mathf.Lerp(0, _maxShakeSpeed, _normShake);
        float shakeMagnitude = Mathf.Lerp(0, _maxShakeMagnitude, _normShake);

        float shakeX = (Mathf.Sin((Time.time + perlinX) * shakeSpeed) * shakeMagnitude);
        float shakeY = (Mathf.Sin((Time.time + perlinY) * shakeSpeed) * shakeMagnitude);
        transform.localPosition = new Vector2(shakeX, shakeY);
    }
}
