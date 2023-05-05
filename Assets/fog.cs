using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;

public class fog : MonoBehaviour
{
    private MongoClient mongoClient;
    private IMongoDatabase db;
    private IMongoCollection<BsonDocument> collection;
    private IEnumerator<BsonDocument> cursor;
    

    void Start()
    {
        string connectionString = "mongodb+srv://asadtariq1999:virtyou@testingvirtyou.ner4fbz.mongodb.net/?retryWrites=true&w=majority";
        
        mongoClient = new MongoClient(connectionString);
        
        db = mongoClient.GetDatabase("sensors");

        // Get a reference to the "readings" collection
        collection = db.GetCollection<BsonDocument>("readings");

        // Get a cursor to the first document
        cursor = collection.FindSync(new BsonDocument()).ToEnumerable().GetEnumerator();
        StartCoroutine(UpdateFog());
    }

    private IEnumerator UpdateFog()
    {

        while (true)
        {
            // If there are more documents to read, read the next one
            if (cursor.MoveNext())
            {
                var document = cursor.Current;
                int co2Level = (int) document[1]["co2"];
                Debug.Log(co2Level);
                
                if (co2Level > 200)
                {
                    RenderSettings.fog = true;
                    RenderSettings.fogColor = Color.red;
                }
                else
                {
                    RenderSettings.fogColor = Color.green;
                }
            }
            
            else // If we have reached the end of the cursor, reset it
            {
                cursor.Dispose();
                cursor = collection.FindSync(new BsonDocument()).ToEnumerable().GetEnumerator();
            }

            // Wait for 5 seconds before executing the next iteration of the coroutine
            yield return new WaitForSeconds(5);
        }
    }
}