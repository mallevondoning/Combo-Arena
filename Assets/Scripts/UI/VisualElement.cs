using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualElement : MonoBehaviour
{
    [SerializeField] private Element[] _element;
    [SerializeField] private Image _elementImage;

    [SerializeField] private ElementType _elementType;

    private void OnValidate()
    {
        switch (_elementType)
        {
            case ElementType.fire:
                _elementImage.sprite = _element[0].ElementSprite;
                break;
            case ElementType.lighting:
                _elementImage.sprite = _element[1].ElementSprite;
                break;
            case ElementType.ice:
                _elementImage.sprite = _element[2].ElementSprite;
                break;
            case ElementType.NoneID:
                Debug.LogWarning(transform.parent.name + " has no element type");
                break;
            default:
                Debug.LogError("somthing horribale happed to " + transform.parent.name);
                break;
        }
    }
}
