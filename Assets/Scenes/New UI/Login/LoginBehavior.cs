using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class LoginBehavior : MonoBehaviour
{
    // checking node.js
    string geturl = "http://127.0.0.1:3000/getuser";
    string posturl = "http://127.0.0.1:3000/adduser";
    string userDetails = "";
    public void getText()
    {
        
        StartCoroutine(GetTheText());
    }
    IEnumerator GetTheText()
    {
        WWW www = new WWW(geturl);
        
        yield return www;
        Debug.Log(www.text);
        
    }

        public void getUser()
    {
        Debug.Log("Login button pressed");
        string id = ObjectId.GenerateNewId().ToString();
        string emailuser = "aliza.khorasi@gmail.com";
        string passworduser = "virtyou123";

        StartCoroutine(getUserQuery(id, emailuser, passworduser));
    }

    IEnumerator getUserQuery(string id, string emailuser, string passworduser)
{
    // Create a JSON string manually
string jsonStr = "{\"_id\":\"" + id + "\",\"password\":\"" + passworduser + "\",\"name\":\"" + emailuser + "\"}";

    // Create a UnityWebRequest instance
    UnityWebRequest www = new UnityWebRequest(geturl, "POST");
    byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonStr);
    www.uploadHandler = new UploadHandlerRaw(jsonBytes);
    www.SetRequestHeader("Content-Type", "application/json");

    // Send the request and wait for the response
    yield return www.SendWebRequest();

    if (www.result == UnityWebRequest.Result.Success)
    {
        Debug.Log("JSON data sent successfully!");
        Debug.Log("Response: " + www.downloadHandler.text);
    }
    else
    {
        Debug.LogError("Failed to send JSON data: " + www.error);
    }

    // Clean up the UnityWebRequest
    www.Dispose();
}

    public void setText()
    {
        string id = ObjectId.GenerateNewId().ToString();
        string emailuser = "check1@gmail.com";
        string passworduser = "1234";
        string nameuser = "check23";
        string roleuser = "admin";

        StartCoroutine(SetTheText(id, emailuser, passworduser, nameuser, roleuser));
    }

    IEnumerator SetTheText(string id, string emailuser, string passworduser, string nameuser, string roleuser)
{
    // Create a JSON string manually
    string jsonStr = "{\"_id\":\"" + id + "\",\"password\":\"" + passworduser + "\",\"name\":\"" + nameuser + "\",\"email\":\"" + emailuser + "\",\"role\":\"" + roleuser + "\"}";

    // Create a UnityWebRequest instance
    UnityWebRequest www = new UnityWebRequest(posturl, "POST");
    byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonStr);
    www.uploadHandler = new UploadHandlerRaw(jsonBytes);
    www.SetRequestHeader("Content-Type", "application/json");

    // Send the request and wait for the response
    yield return www.SendWebRequest();

    if (www.result == UnityWebRequest.Result.Success)
    {
        Debug.Log("JSON data sent successfully!");
        Debug.Log("Response: " + www.downloadHandler.text);
    }
    else
    {
        Debug.LogError("Failed to send JSON data: " + www.error);
    }

    // Clean up the UnityWebRequest
    www.Dispose();
}




    public TMP_InputField email;
    public string emailText;
    public TMP_InputField password;
    public string passwordText;
    private MongoClient mongoClient;
    private IMongoDatabase db;
    public static bool userRole;

    private void connection()
    {
        string connectionString = "mongodb+srv://asadtariq1999:virtyou@testingvirtyou.ner4fbz.mongodb.net/?retryWrites=true&w=majority";
        Debug.Log("Estabilishing connection...");
        mongoClient = new MongoClient(connectionString);
        Debug.Log("Connection Established...");
        db = mongoClient.GetDatabase("Authentication");
    }

    private void authenticate(string email, string password)
    {
        connection();
        var collection = db.GetCollection<BsonDocument>("users");
        Debug.Log("Collection found");
        var filter = Builders<BsonDocument>.Filter.Eq("email", email) &
              Builders<BsonDocument>.Filter.Eq("password", password);
        List<BsonDocument> results = collection.Find(filter).ToList();
        // done authentication
        

        if (results.Count > 0)
        {
            Debug.Log("Login Successful");
            if (results[0]["role"].ToString() == "admin")
            {
                userRole = true;
            }
            else
            {
                userRole = false;
            }
            SceneManager.LoadScene("MenuModified");
        }
        else
        {
            Debug.Log("Login Failed");
        }
       
    }

    public void Login()
    {
        Debug.Log("Login button pressed");
        // emailText = email.text;
        // passwordText = password.text;

        // if (emailText == "aliza.khorasi@gmail.com" & passwordText == "virtyou123")
        // {
        //     SceneManager.LoadScene("MenuModified");
        // }

        //getText();
        getUser();
        //setText();

        //authenticate(emailText, passwordText);
    }
}
