using UnityEngine;
using System;
using System.Collections;

public class WrapAround : MonoBehaviour
{
    [SerializeField]
    float offScreenPixels;
    Camera cam;
    Action<Vector3> onWrapAction;


    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void AddOnWrapAction(Action<Vector3> callback)
    {
        onWrapAction += callback;
    }

    void Update()
    {
        bool wrap = false;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        if (screenPos.x < -offScreenPixels)
        {
            screenPos.x = Screen.width + offScreenPixels * 0.90f;
            wrap = true;
        }
        else if (screenPos.x > Screen.width + offScreenPixels)
        {
            screenPos.x = -offScreenPixels + offScreenPixels * 0.90f;
            wrap = true;
        }
        if (screenPos.y < -offScreenPixels)
        {
            screenPos.y = Screen.height + offScreenPixels * 0.90f;
            wrap = true;
        }
        else if (screenPos.y > Screen.height + offScreenPixels)
        {
            screenPos.y = -offScreenPixels + offScreenPixels * 0.90f;
            wrap = true;
        }
        if (wrap)
        {
            Vector3 newPosition = cam.ScreenToWorldPoint(screenPos);
            Vector3 translation = newPosition - transform.position;
            transform.position = newPosition;
            if(onWrapAction != null)
            {
                onWrapAction(translation);
            }
            
        }
    }
}
