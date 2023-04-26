using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using MongoDB.Bson;

public class LoginBehavior : MonoBehaviour
{
    // checking node.js
    string geturl =  "https://flask-mongo-backend-ar230500-famas.vercel.app/getuser";
    string posturl = "https://flask-mongo-backend-ar230500-famas.vercel.app/adduser";
    string testinggeturl = "http://127.0.0.1:5000/getuser/";
    string testingposturl = "http://127.0.0.1:5000/adduser/";
    string userDetails = "";

    public TMP_InputField email;
    public string emailText;
    public TMP_InputField password;
    public string passwordText;
    public static bool userRole;

    public void getUser()
    {
        Debug.Log("Login button pressed");
        string id = ObjectId.GenerateNewId().ToString();
        
        StartCoroutine(GetUserQuery(emailText, passwordText));
    }

    IEnumerator GetUserQuery(string email, string password)
    {
        string url = "https://flask-mongo-backend-ar230500-famas.vercel.app/getuser/?email=" + email + "&password=" + password;
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Request successful!");
                if (www.downloadHandler.text == "does not exist")
                {
                    Debug.Log("No user found");
                }
                else
                {
                    Debug.Log("Response: " + www.downloadHandler.text);
                    SceneManager.LoadScene("MenuModified");
                }
                Debug.Log("Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Request failed: " + www.error);
            }
        }
    }


    public void Login()
    {
        Debug.Log("Login button pressed");
        emailText = email.text;
        passwordText = password.text;

        // if (emailText == "aliza.khorasi@gmail.com" & passwordText == "virtyou123")
        // {
        //     SceneManager.LoadScene("MenuModified");
        // }

        getUser();
        //setText();

        //authenticate(emailText, passwordText);
    }
}
