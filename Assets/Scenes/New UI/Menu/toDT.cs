using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class toDT : MonoBehaviour
{
    public void StartXR()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    }

    public void toDTpage()
    {
        StartXR();
        SceneManager.LoadScene("SampleScene");
    }
}
