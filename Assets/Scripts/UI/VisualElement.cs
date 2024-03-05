using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualElement : MonoBehaviour
{
    public Element Element { get; set; } = null;

    [SerializeField] private Image _elementImage;
    [SerializeField] private Image _borderImage;

    private void OnValidate()
    {
        if (Element == null) return;
 
        _elementImage.sprite = Element.ElementSprite;
        _borderImage.color = Element.BorderColor;
    }

    private void Update()
    {
        if (Element == null) return;

        _elementImage.sprite = Element.ElementSprite;
        _borderImage.color = Element.BorderColor;
    }
}
