using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EaseIn : IEase
{
    [SerializeField][Range(1f, 0f)] private float _easeInFrom = 0f;
    [SerializeField][Range(1f, 0f)] private float _easeInTo = 0.1f;
    [SerializeField][Range(1f, 0f)] private float _maxRange = 1f;

    public float Ease(float value, float modifier)
    {
        throw new NotImplementedException();
    }

    public float Ease01(float value)
    {
        throw new NotImplementedException();
    }
}
