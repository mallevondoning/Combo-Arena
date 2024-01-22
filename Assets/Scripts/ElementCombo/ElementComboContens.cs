using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Data/Projectile")]
public class ElementComboContens : ScriptableObject
{
    public AnimationClip Animation; //<--- switch to right information
    public List<ProjectileContens> Contens;
}
