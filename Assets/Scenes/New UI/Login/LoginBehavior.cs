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

    public TMP_InputField email;
    public string emailText;
    public TMP_InputField password;
    public string passwordText;
    public static string userRole;

    public void getUser()
    {
        Debug.Log("Login button pressed");
        
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
                    userRole = www.downloadHandler.text;
                    Debug.Log("UserRole: " + userRole);
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
        getUser();
    }
}
