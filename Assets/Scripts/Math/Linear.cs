using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Linear : IEase, ISerializationCallbackReceiver
{
    [SerializeField][Range(1f, 0f)] private float _maxRange = 1f;
    [SerializeField][Range(1f, 0f)] private float _minRange = 0f;

    private void OnValidate()
    {
        _maxRange = Mathf.Clamp(_maxRange, _minRange, 1);
        _minRange = Mathf.Clamp(_minRange, 0, _maxRange);
    }

    public float Ease01(float normValue)
    {
        float norm = (normValue - _minRange) / (_maxRange - _minRange);
        return norm;
    }
    public float Ease(float value, float modifier)
    {
        float norm = (value - (_minRange * modifier)) / (_maxRange - _minRange) * modifier;
        return norm;
    }

    public void OnAfterDeserialize() => OnValidate();

    public void OnBeforeSerialize() { }

}
