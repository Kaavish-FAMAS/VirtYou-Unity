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
        relayOutput();
    }

    public void relayOutput()
    {
        StartCoroutine(sendRelayOutput());
    }

    IEnumerator sendRelayOutput()
    {
    //     if (LoginBehavior.userRole == "admin")
    //     {
    //         if (Input.GetButtonDown("B"))
    //         {
    //             Debug.Log("button B pressed");
    //             if (relayOneOn == false)
    //             {
    //                 url = "https://flask-mongo-backend-ar230500-famas.vercel.app/adduser/?out_one=" + 1 + "&out_two=" + 0 + "&id=" + ObjectId.GenerateNewId();
    //                 relayOneOn = true;
    //             }
    //             else
    //             {
    //                 url = "https://flask-mongo-backend-ar230500-famas.vercel.app/adduser/?out_one=" + 0 + "&out_two=" + 0 + "&id=" + ObjectId.GenerateNewId();
    //             }
    //         }
    //         else if (Input.GetButtonDown("Y"))
    //         {
    //             if (relayTwoOn == false)
    //             {
    //                 url = "https://flask-mongo-backend-ar230500-famas.vercel.app/adduser/?out_one=" + 0 + "&out_two=" + 1 + "&id=" + ObjectId.GenerateNewId();
    //                 relayTwoOn = true;
    //             }
    //             else
    //             {
    //                 url = "https://flask-mongo-backend-ar230500-famas.vercel.app/adduser/?out_one=" + 0 + "&out_two=" + 0 + "&id=" + ObjectId.GenerateNewId();
    //             }
    //         }

    //         using (UnityWebRequest www = UnityWebRequest.Get(url))
    //         {
    //             yield return www.SendWebRequest();

    //             if (www.result == UnityWebRequest.Result.Success)
    //             {
    //                 Debug.Log("Request successful!");
    //                 if (www.downloadHandler.text == "Relay Outputs Configured")
    //                 {
    //                 Debug.Log("Response: " + www.downloadHandler.text);
    //                 }
    //                 else
    //                 {
    //                     Debug.Log("Error");
    //                 }
    //             }
    //             else
    //             {
    //                 Debug.LogError("Request failed: " + www.error);
    //             }
    //         }

    //         yield return new WaitForSeconds(5);
    //     }
        if (Input.GetButtonDown("B"))
            {
                Debug.Log("button B pressed");
                if (relayOneOn == false)
                {
                    url = "https://flask-mongo-backend-ar230500-famas.vercel.app/addrelayout/?out_one=1&out_two=0&id=" + ObjectId.GenerateNewId();
                    relayOneOn = true;
                }
                else
                {
                    url = "https://flask-mongo-backend-ar230500-famas.vercel.app/addrelayout/?out_one=" + "0" + "&out_two=" + "0" + "&id=" + ObjectId.GenerateNewId();
                    relayOneOn = false;
                }
            }
            else if (Input.GetButtonDown("Y"))
            {
                if (relayTwoOn == false)
                {
                    url = "https://flask-mongo-backend-ar230500-famas.vercel.app/addrelayout/?out_one=" + "0" + "&out_two=" + "1" + "&id=" + ObjectId.GenerateNewId();
                    relayTwoOn = true;
                }
                else
                {
                    url = "https://flask-mongo-backend-ar230500-famas.vercel.app/addrelayout/?out_one=" + "0" + "&out_two=" + "0" + "&id=" + ObjectId.GenerateNewId();
                }
            }

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
