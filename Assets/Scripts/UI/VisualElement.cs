using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualElement : MonoBehaviour
{
    [SerializeField] private Element _element;
    [SerializeField] private Image _elementImage;
    [SerializeField] private Image _borderImage;

    private void OnValidate()
    {
        _elementImage.sprite = _element.ElementSprite;
        _borderImage.color = _element.BorderColor;
    }
}
