using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;



public class signup : MonoBehaviour
{
    public TMP_InputField name;
    public string nameText;
    public TMP_InputField email;
    public string emailText;
    public TMP_InputField password;
    public string passwordText;
    public Toggle value;
    public string toggleValue;
    public Image errorImage;
    private MongoClient mongoClient;
    private IMongoDatabase db;


    public bool verify(string name, string email, string password)
    {
        if (name != "" && email != "" && password != "")
        {
            return true;
        }
        else if (email.Contains("@") && email.Contains(".com"))
        {
            return true;
        }
        return false;
    }

    public void createUser()
    {

        StartCoroutine(CreateTheUser(emailText, passwordText, nameText, toggleValue));
    }

    IEnumerator CreateTheUser(string emailuser, string passworduser, string nameuser, string roleuser)
    {
       string url = "https://flask-mongo-backend-ar230500-famas.vercel.app/adduser/?email=" + emailuser + "&password=" + passworduser + "&name=" + nameuser + "&role=" + roleuser + "&id=" + ObjectId.GenerateNewId();
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Request successful!");
                if (www.downloadHandler.text == "User Added")
                {
                   Debug.Log("Response: " + www.downloadHandler.text);
                    SceneManager.LoadScene("LoginModified");
                }
                else
                {
                    Debug.Log("Error");
                }
                Debug.Log("Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Request failed: " + www.error);
            }
        }
    }

    public void Register()
    {
        if (value.isOn) 
        {
            toggleValue = "admin";
        }
        else 
        {
            toggleValue = "user";
        }
        nameText = name.text;
        emailText = email.text;
        passwordText = password.text;
        if (verify(nameText, emailText, passwordText))
        {
            createUser();
        }
        else
        {
            errorImage.gameObject.SetActive(true);
            
        }
        
        
    }
}
