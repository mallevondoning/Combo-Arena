using System;
using UnityEngine;

[Serializable]
public class ProjectileModifier
{
    public bool DestroyOnContact = true;
    [Header("")]
    public ProjectileEffects ModfilerEvent = new ProjectileEffects();
}
