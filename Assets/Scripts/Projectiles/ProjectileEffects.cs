using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectileEffects
{
    public ProjectileEffects Effect;

    protected virtual void SelfDestroy()
    {
        //Physics.Raycast();
    }

    public virtual void OnHit()
    {

    }
}
