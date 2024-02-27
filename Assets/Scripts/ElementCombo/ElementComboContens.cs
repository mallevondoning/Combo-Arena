using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

[CreateAssetMenu(menuName = "Game/Data/Projectile")]
public class ElementComboContens : ScriptableObject
{
    [Header("Combo")]
    public ElementType[] ElementCombo = { ElementType.NoneID, ElementType.NoneID, ElementType.NoneID };
    [Header("Animation")]
    public AnimationClip Animation; //<--- switch to right information
    [Range(0,1)][Tooltip("Spawns the projectile comparetly to the ainmation. 0 is the start of the animation, 1 is the end of the animation.")] public float WhenToSpawn = 1f;
    [Header("Projectile contens")]
    public List<ProjectileContens> Contens;

    private void OnValidate()
    {
        if (ElementCombo.Length > 3) Debug.LogWarning("The "+ToString()+" combo is to long. MAX IS 3");
        if (ElementCombo.Length < 3) Debug.LogWarning("The " + ToString() + " combo is to short. MIN IS 3");
    }

    public override string ToString()
    {
        string elementComboStr = "";
        for (int i = 0; i < ElementCombo.Length; i++)
        {
            if (ElementCombo[i] == ElementType.NoneID)
            {
                break;
            }

            switch (ElementCombo[i])
            {
                case ElementType.fire:
                    elementComboStr += "F ";
                    break;
                case ElementType.lighting:
                    elementComboStr += "L ";
                    break;
                case ElementType.ice:
                    elementComboStr += "I ";
                    break;
            }
        }

        return elementComboStr;
    }
}
