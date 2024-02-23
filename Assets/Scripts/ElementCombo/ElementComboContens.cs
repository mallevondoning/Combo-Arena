using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Data/Projectile")]
public class ElementComboContens : ScriptableObject
{
    public AnimationClip Animation; //<--- switch to right information
    public ElementType[] ElementCombo = { ElementType.NoneID, ElementType.NoneID, ElementType.NoneID };
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
