using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class cameraSwitch : MonoBehaviour
{
    public Camera normalCamera;
    public Camera birdseyeCamera;

    private bool isBirdseyeView = false;

    private void Start()
    {
        normalCamera.enabled = true;
        birdseyeCamera.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Change this to the input you want to use to switch between views
        {
            isBirdseyeView = !isBirdseyeView;

            if (isBirdseyeView)
            {
                EnableBirdseyeView();
            }
            else
            {
                DisableBirdseyeView();
            }
        }
    }

    private void EnableBirdseyeView()
    {
        normalCamera.enabled = false;

        //Vector3 birdseyePos = new Vector3(normalCamera.transform.position.x, birdseyeHeight, normalCamera.transform.position.z);
        //birdseyeCamera.transform.position = birdseyePos;
        //birdseyeCamera.orthographicSize = birdseyeSize;

        birdseyeCamera.enabled = true;
        XRSettings.showDeviceView = false;
    }

    private void DisableBirdseyeView()
    {
        birdseyeCamera.enabled = false;
        normalCamera.enabled = true;
        XRSettings.showDeviceView = true;
    }
}

