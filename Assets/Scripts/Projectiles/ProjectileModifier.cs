using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ProjectileModifier
{
    public bool DestroyOnContact = true;
    [Header("")]
    public UnityEvent ModfilerEvent = new UnityEvent();
}
