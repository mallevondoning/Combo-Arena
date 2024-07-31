using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseInOut : IEase,ISerializationCallbackReceiver
{
    [SerializeField][Range(1f, 0f)] private float _easeInFrom = 0f;
    [SerializeField][Range(1f, 0f)] private float _easeInTo = 0.1f;
    [SerializeField][Range(1f, 0f)] private float _easeOutFrom = 0.9f;
    [SerializeField][Range(1f, 0f)] private float _easeOutTo = 1f;

    private void OnVaildate()
    {
        _easeInFrom = Mathf.Clamp(_easeInFrom, 0, _easeInTo);
        _easeInTo = Mathf.Clamp(_easeInTo, _easeInFrom, _easeOutFrom);
        _easeOutFrom = Mathf.Clamp(_easeOutFrom, _easeInTo, _easeOutTo);
        _easeOutTo = Mathf.Clamp(_easeOutTo, _easeOutFrom, 1);
    }

    public float Ease(float value, float modifier)
    {
        throw new NotImplementedException();
    }

    public float Ease01(float value)
    {
        throw new NotImplementedException();
    }

    public void OnAfterDeserialize() => OnVaildate();

    public void OnBeforeSerialize() { }
}
