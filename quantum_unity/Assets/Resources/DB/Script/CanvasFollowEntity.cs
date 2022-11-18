using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollowEntity : MonoBehaviour
{
    //[SerializeField] private GameObject pointingIcon;
    [SerializeField] private Vector3 offset;

    [Tooltip("World space object to follow")]
    public GameObject target;
    [Tooltip("World space camera that renders the target")]
    public Camera worldCamera;
    [Tooltip("Canvas set in Screen Space Camera mode")]
    public Canvas canvas;
    
    private void Start()
    {
        worldCamera = Camera.main;
        canvas.worldCamera = worldCamera;
    }



    private void LateUpdate()
    {
        var rt = GetComponent<RectTransform>();
        RectTransform parent = (RectTransform)rt.parent;
        var vp = worldCamera.WorldToViewportPoint(target.transform.position);
        var sp = canvas.worldCamera.ViewportToScreenPoint(vp);
        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(parent, sp, canvas.worldCamera, out worldPoint);
        rt.position = worldPoint + offset;

        UpdateHealthColor();
    }

    private void UpdateHealthColor()
    {

    }
}