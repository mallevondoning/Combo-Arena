using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElementComboManager : MonoBehaviour
{
    public static ElementComboManager Instance { get; set; }
    public List<ElementComboContens> ECContens = new List<ElementComboContens>();

    private void Awake()
    {
        Instance = this;
    }
}
