using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    [SerializeField] private RectTransform ui;

    private Vector3 offset;
    private float scale = 1;

    private void Update()
    {
        var mousePos = Input.mousePosition;

        scale = Mathf.Clamp(scale + Input.GetAxis("Mouse ScrollWheel"), 0.5f, 1.5f);
        ui.localScale = Vector3.one * scale;

        if(Input.GetMouseButtonDown(1))
        {
            offset = mousePos - ui.localPosition;
        }
        if(Input.GetMouseButton(1))
        {
            ui.localPosition = mousePos - offset;
        }
    }
}
