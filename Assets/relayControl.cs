using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using MongoDB.Bson;

public class relayControl : MonoBehaviour
{
    private bool relayOneOn = false;
    private bool relayTwoOn = false;
    private string url = "";

    void Update()
    {
        if (LoginBehavior.userRole == "admin")
        {
            if (Input.GetButtonDown("B"))
            {
                Debug.Log("button B pressed");
                if (relayOneOn == false)
                {
                    url = "https://flask-mongo-backend-ar230500-famas.vercel.app/addrelayout/?out_one=1&out_two=0";
                    relayOneOn = true;
                }
                else
                {
                    url = "https://flask-mongo-backend-ar230500-famas.vercel.app/addrelayout/?out_one=0&out_two=0";
                    relayOneOn = false;
                }
                StartCoroutine(sendRelayOutput());
            }
            else if (Input.GetButtonDown("Y"))
            {
                Debug.Log("button Y pressed");
                if (relayTwoOn == false)
                {
                    url = "https://flask-mongo-backend-ar230500-famas.vercel.app/addrelayout/?out_one=0&out_two=1";
                    relayTwoOn = true;
                }
                else
                {
                    url = "https://flask-mongo-backend-ar230500-famas.vercel.app/addrelayout/?out_one=0&out_two=0";
                    relayTwoOn = false;
                }
                StartCoroutine(sendRelayOutput());
            }
        }
    }

    IEnumerator sendRelayOutput()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Request successful!");
                if (www.downloadHandler.text == "Relay Outputs Configured")
                {
                Debug.Log("Response: " + www.downloadHandler.text);
                }
                else
                {
                    Debug.Log("Error");
                }
            }
            else
            {
                Debug.LogError("Request failed: " + www.error);
            }
        }

        yield return new WaitForSeconds(5);
    }
}
