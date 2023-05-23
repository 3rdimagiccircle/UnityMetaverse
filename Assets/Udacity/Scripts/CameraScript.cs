using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    bool mouseDown = false;
    float mouseX;
    float mouseY;


    //  public GameObject player;

    Camera mainCamera;

    // float MouseZoomSpeed = 60.0f;
    float TouchZoomSpeed = 0.1f;
    float ZoomMinBound = 20.0f;
    float ZoomMaxBound = 90.0f;
    //Camera cam;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        //if (!UIHandler.isPopUPsEnable)
        //{
        if (Input.GetMouseButtonDown(0) && !mouseDown)
        {
            mouseDown = true;

            mouseX = Input.mousePosition.x;
            mouseY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0) && mouseDown)
        {
            mouseDown = false;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Zoom(scroll, TouchZoomSpeed);
    }
    //}

    void LateUpdate()
    {
        //if (!UIHandler.isPopUPsEnable)
        //{
        if (mouseDown)
        {
            float mouseXStop = Input.mousePosition.x;
            float mouseYStop = Input.mousePosition.y;
            float deltaX = mouseXStop - mouseX;
            float deltaY = mouseYStop - mouseY;
            float centerXNew = Screen.width / 2 + deltaX;
            float centerYNew = Screen.height / 2 + deltaY;

            Vector3 Gaze = mainCamera.ScreenToWorldPoint(new Vector3(centerXNew, centerYNew, mainCamera.nearClipPlane));
            transform.LookAt(Gaze);
            mouseX = mouseXStop;
            mouseY = mouseYStop;
        }
        //transform.LookAt(player.transform);
        //transform.position = player.transform.position;
    }
    //   }


    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        mainCamera.fieldOfView -= deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, ZoomMinBound, ZoomMaxBound);
    }
}
