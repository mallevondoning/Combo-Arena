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

    private float _normFunc;

    private void Awake()
    {
        switch (_statusType)
        {
            case StatusElementType.Ablaze:
                _normFunc = GameManager.Instance.PlayerStatus.NormAblaze(_shakeThershold);
                break;
            case StatusElementType.Shocked:
                _normFunc = GameManager.Instance.PlayerStatus.NormShocked(_shakeThershold);
                break;
            case StatusElementType.Frozen:
                _normFunc = GameManager.Instance.PlayerStatus.NormFrozen(_shakeThershold);
                break;
        }
    }

    private void Update()
    {
        _img.color = _statusGradient.Evaluate(_normFunc);

        if (_img.color.a > _shakeThershold) Shake();
        else Move();
    }

    private Vector2 Move()
    {
        return Vector2.zero;
    }
    private Vector2 Shake()
    {
        return Vector2.zero;
    }
}
