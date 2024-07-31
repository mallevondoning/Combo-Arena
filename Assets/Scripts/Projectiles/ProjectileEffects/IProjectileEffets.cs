using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileEffets
{
    void Init(Vector3 hitPos, BulletTeam bulletTeam); //<--- explosion,

    void Action();
}
