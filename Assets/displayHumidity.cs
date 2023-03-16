using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.UI;

public class displayHumidity : MonoBehaviour
{
    private MongoClient mongoClient;
    private IMongoDatabase db;
    // private List<BsonDocument> results;
    // private BsonDocument cursor;
    private IMongoCollection<BsonDocument> collection;
    private double humidity;

    public Material sphereMaterial;

    // Start is called before the first frame update
    void Start()
    {
        string connectionString = "mongodb+srv://asadtariq1999:virtyou@testingvirtyou.ner4fbz.mongodb.net/?retryWrites=true&w=majority";
        mongoClient = new MongoClient(connectionString);
        db = mongoClient.GetDatabase("sensors");
    }

    /*private void QueryCollection()
    {
        // Get a reference to the "mycollection" collection
        collection = db.GetCollection<BsonDocument>("readings");

        // Use the filter to query the collection and get a list of matching documents
        var cursor = collection.FindSync(new BsonDocument());

        while (cursor.MoveNext())
        {
            foreach (var document in cursor.Current)
            {
                humidity = document[1]["humidity"].ToString();
                Debug.Log($"Humdiity: {humidity}");
            }
        }

        cursor.Dispose();

        // Print the results to the console
        // foreach (BsonDocument result in results)
        // {
        //     Debug.Log(result[1]["humidity"].ToJson());
        // }
    }*/

    // Update is called once per frame
    void Update()
    {
        // Get a reference to the "mycollection" collection
        collection = db.GetCollection<BsonDocument>("readings");
        var cursor = collection.FindSync(new BsonDocument());
        while (cursor.MoveNext())
        {
            var currentBatch = cursor.Current;
            foreach (var document in currentBatch)
            {
                var humidity = document[1]["humidity"].ToDouble();
                Debug.Log(humidity);
                if (humidity > 50.0)
                {
                    sphereMaterial.SetColor("_Color", Color.red);
                }
                else
                {
                    sphereMaterial.SetColor("_Color", Color.green);
                }
            }
        }

        cursor.Dispose();
    }
}