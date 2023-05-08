using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using TMPro;
using System;
using UnityEngine.Networking;

public class displayHumidity : MonoBehaviour
{
    private IMongoCollection<BsonDocument> collection;
    private IEnumerator<BsonDocument> cursor;
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        string apiUrl = "https://flask-mongo-backend-ar230500-famas.vercel.app/getsensor/";

        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Start the coroutine to update the color every 15 seconds
        StartCoroutine(UpdateTextCoroutine(apiUrl));
    }

    private IEnumerator UpdateTextCoroutine(string apiUrl)
    {
        while (true)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log("Error: " + webRequest.error);
                }
                else
                {
                    string json = webRequest.downloadHandler.text;
                    List<BsonDocument> readings = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<BsonDocument>>(json);

                    if (readings != null && readings.Count > 0)
                    {

                        // Iterate over the readings and display the humidity for each one
                        foreach (BsonDocument reading in readings)
                        {
                            double humidityVal = reading[1]["humidity"].ToDouble();
                            textMeshPro.text = "Humidity \n" + humidityVal.ToString("0.00") + "%";
                            yield return new WaitForSeconds(5);
                        }
                    }
                    else
                    {
                        Debug.Log("No sensor data found");
                    }
                }
            }

            // Wait for 15 seconds before executing the next iteration of the coroutine
            yield return new WaitForSeconds(5);
        }
    }
}
