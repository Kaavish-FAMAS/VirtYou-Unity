using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class cameraSwitch : MonoBehaviour
{
    public Camera normalCamera;
    public Camera birdseyeCamera;
    public GameObject firstPlane;
    public GameObject secondPlane;
    public GameObject thirdPlane;
    public GameObject fourthPlane;

    private bool isBirdseyeView = false;

    private void Start()
    {
        normalCamera.enabled = true;
        birdseyeCamera.enabled = false;
        // Disable plane renderer component by default
        firstPlane.GetComponent<MeshRenderer>().enabled = false;
        secondPlane.GetComponent<MeshRenderer>().enabled = false;
        thirdPlane.GetComponent<MeshRenderer>().enabled = false;
        fourthPlane.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("hit") || Input.GetKeyDown(KeyCode.Space)) // Change this to the input you want to use to switch between views
        {
            isBirdseyeView = !isBirdseyeView;

            if (isBirdseyeView)
            {
                RenderSettings.fog = false;
                EnableBirdseyeView();
            }
            else
            {
                RenderSettings.fog = true;
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
        birdseyeCamera.transform.position = new Vector3(10f, 23.24f, -11.08f);
        birdseyeCamera.transform.rotation = Quaternion.Euler(89.878f, -82.744f, 7.527f);
        XRSettings.showDeviceView = false;
        // Enable plane renderer component when switching to bird's eye view camera
        firstPlane.GetComponent<MeshRenderer>().enabled = true;
        secondPlane.GetComponent<MeshRenderer>().enabled = true;
        thirdPlane.GetComponent<MeshRenderer>().enabled = true;
        fourthPlane.GetComponent<MeshRenderer>().enabled = true;
    }

    private void DisableBirdseyeView()
    {
        birdseyeCamera.enabled = false;
        normalCamera.enabled = true;
        XRSettings.showDeviceView = true;
        // Disable plane renderer component when switching to normal camera
        firstPlane.GetComponent<MeshRenderer>().enabled = false;
        secondPlane.GetComponent<MeshRenderer>().enabled = false;
        thirdPlane.GetComponent<MeshRenderer>().enabled = false;
        fourthPlane.GetComponent<MeshRenderer>().enabled = false;
    }
}
