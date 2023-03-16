using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.SceneManagement;



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

    private void connection()
    {
        string connectionString = "mongodb+srv://asadtariq1999:virtyou@testingvirtyou.ner4fbz.mongodb.net/?retryWrites=true&w=majority";
        Debug.Log("Estabilishing connection...");
        mongoClient = new MongoClient(connectionString);
        Debug.Log("Connection Established...");
        db = mongoClient.GetDatabase("Authentication");
    }

    private void updateDB()
    {
        connection();
        var collection = db.GetCollection<BsonDocument>("users");
        var document = new BsonDocument
        {
            {"_id", ObjectId.GenerateNewId()},
            {"name", nameText},
            {"email", emailText},
            {"password", passwordText},
            {"role", toggleValue}
        };
        collection.InsertOne(document);
        SceneManager.LoadScene("LoginModified");
        Debug.Log("Data Inserted...");
    }

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
            updateDB();
        }
        else
        {
            errorImage.gameObject.SetActive(true);
            
        }
        
        
    }
}
