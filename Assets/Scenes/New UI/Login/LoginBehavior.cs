using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.SceneManagement;

public class LoginBehavior : MonoBehaviour
{
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
        emailText = email.text;
        passwordText = password.text;

        if (emailText == "aliza.khorasi@gmail.com" & passwordText == "virtyou123")
        {
            SceneManager.LoadScene("MenuModified");
        }

        //authenticate(emailText, passwordText);
    }
}
