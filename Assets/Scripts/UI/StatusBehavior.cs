using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBehavior : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image _img;
    [SerializeField] private Rigidbody2D _rb;

    [Header("Status components")]
    [SerializeField] private StatusElementType _statusType;
    [SerializeField] private Gradient _statusGradient;

    [Header("Varibles")]
    [SerializeField][Range(0, 1)] private float _shakeThershold = 0.9f;
    [SerializeField] private float _maxShakeSpeed = 25;
    [SerializeField] private float _maxShakeMagnitude = 35;

    private Vector3 startPos;

    private float _normAlpha;
    private float _normShake;

    private float _moveSpeed;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        FindElement(_statusType);

        _img.color = _statusGradient.Evaluate(_normAlpha);

        if (_img.color.a > _shakeThershold) Shake();
        else
        {
            transform.position = startPos;
            Move();
        }
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
                _normShake = GameManager.Instance.PlayerStatus.NormAblaze(_shakeThershold, false);
                break;
            case StatusElementType.Frozen:
                _normAlpha = GameManager.Instance.PlayerStatus.NormFrozen(_shakeThershold, true);
                _normShake = GameManager.Instance.PlayerStatus.NormAblaze(_shakeThershold, false);
                break;
        }
    }

    private Vector2 Move()
    {
        return Vector2.zero;
    }
    private void Shake()
    {
        float perlinX = Mathf.PerlinNoise1D(Time.time);
        float perlinY = Mathf.PerlinNoise1D(Time.time + 1);

        float shakeSpeed = Mathf.Lerp(0, _maxShakeSpeed, _normShake);
        float shakeMagnitude = Mathf.Lerp(0, _maxShakeMagnitude, _normShake);

        float shakeX = startPos.x + Mathf.Sin((Time.time + perlinX) * shakeSpeed) * shakeMagnitude;
        float shakeY = startPos.y + Mathf.Sin((Time.time + perlinY) * shakeSpeed) * shakeMagnitude;
        transform.position = new Vector2(shakeX, shakeY);
    }
}
