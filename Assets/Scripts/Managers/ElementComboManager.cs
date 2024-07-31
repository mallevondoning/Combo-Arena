using System.Collections;
using System.Collections.Generic;
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
