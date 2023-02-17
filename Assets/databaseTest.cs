using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class databaseTest : MonoBehaviour
{
    private MongoClient mongoClient;
    private IMongoDatabase db;

    // Start is called before the first frame update
    void Start()
    {
        string connectionString = "mongodb+srv://shaikh-fahad:UuICDqlZSgoRyNNN@virtyoutest.k97adjo.mongodb.net/?retryWrites=true&w=majority";
        mongoClient = new MongoClient(connectionString);
        db = mongoClient.GetDatabase("sample_mflix");

        // Query collection and print results to the console
        QueryCollection();
    }

    private void QueryCollection()
    {
        // Get a reference to the "mycollection" collection
        var collection = db.GetCollection<BsonDocument>("users");

        // Create a filter to find all documents where the "age" field is greater than or equal to 30
        // var filter = Builders<BsonDocument>.Filter.Gte("age", 30);

        // Use the filter to query the collection and get a list of matching documents
        List<BsonDocument> results = collection.Find(new BsonDocument()).ToList();

        // Print the results to the console
        foreach (BsonDocument result in results)
        {
            Debug.Log(result.ToJson());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
