using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using TMPro;

public class displayHumidity : MonoBehaviour
{
    private MongoClient mongoClient;
    private IMongoDatabase db;
    private IMongoCollection<BsonDocument> collection;
    private IEnumerator<BsonDocument> cursor;
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        string connectionString = "mongodb+srv://asadtariq1999:virtyou@testingvirtyou.ner4fbz.mongodb.net/?retryWrites=true&w=majority";
        
        mongoClient = new MongoClient(connectionString);
        
        db = mongoClient.GetDatabase("sensors");

        // Get a reference to the "readings" collection
        collection = db.GetCollection<BsonDocument>("readings");

        // Get a cursor to the first document
        cursor = collection.FindSync(new BsonDocument()).ToEnumerable().GetEnumerator();

        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Start the coroutine to update the color every 15 seconds
        StartCoroutine(UpdateColorCoroutine());
    }

    private IEnumerator UpdateColorCoroutine()
    {
        while (true)
        {
            // If there are more documents to read, read the next one
            if (cursor.MoveNext())
            {
                var document = cursor.Current;
                double humidityVal = document[1]["humidity"].ToDouble();
                textMeshPro.text = "Humidity \n" + humidityVal.ToString("0.00") + "%";
            }
            else // If we have reached the end of the cursor, reset it
            {
                cursor.Dispose();
                cursor = collection.FindSync(new BsonDocument()).ToEnumerable().GetEnumerator();
            }

            // Wait for 15 seconds before executing the next iteration of the coroutine
            yield return new WaitForSeconds(5);
        }
    }
}