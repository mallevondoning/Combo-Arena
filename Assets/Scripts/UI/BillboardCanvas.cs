using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Update()
    {
        if (_canvas.renderMode == RenderMode.WorldSpace) _canvas.transform.forward = _canvas.worldCamera.transform.forward;
        else Debug.LogWarning("Render mdoe is "+_canvas.renderMode+", so no billboarding");
    }
}
