using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBullet : MonoBehaviour
{
    [SerializeField] private Image _BGPic;
    [SerializeField] private ElementController _ec;
    [SerializeField] private GameObject[] _picObjectList = new GameObject[3];
    private Image[] _picList = new Image[3];

    private Vector4 _combinedColor;

    private void Awake()
    {
        for (int i = 0; i < _picObjectList.Length; i++) _picList[i] = _picObjectList[i].GetComponent<Image>();
    }

    private void Update()
    {
        SetBorderColor();
        //Color newColor = _ec.ElementUI[0].Element.BorderColor + _ec.ElementUI[1].Element.BorderColor;
    }

    private void SetBorderColor()
    {
        _combinedColor += (Vector4)_ec.ElementUI[0].Element.BorderColor;
        _combinedColor.Normalize();
        _combinedColor.z = 1;

        _BGPic.color = _combinedColor;
    }

    //Vector3(-174.5,-87.5,0) <== defualt place
}
