using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EaseOut : IEase
{
    [SerializeField][Range(1f, 0f)] private float _minRange = 0f;
    [SerializeField][Range(1f, 0f)] private float _easeOutFrom = 0.9f;
    [SerializeField][Range(1f, 0f)] private float _easeOutTo = 1f;

    public float Ease(float value, float modifier)
    {
        throw new NotImplementedException();
    }

    public float Ease01(float value)
    {
        throw new NotImplementedException();
    }
}
