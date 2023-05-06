using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.Networking;

public class fog : MonoBehaviour
{
    private IMongoCollection<BsonDocument> collection;
    private IEnumerator<BsonDocument> cursor;
    private Color newColor;

    void Start()
    {
        string apiUrl = "https://flask-mongo-backend-ar230500-famas.vercel.app/getsensor/";

        StartCoroutine(UpdateFog(apiUrl));
    }

    private IEnumerator UpdateFog(string apiUrl)
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
                    // Debug.Log(readings);

                    if (readings != null && readings.Count > 0)
                    {
                        RenderSettings.fog = true;

                        // Iterate over the readings and display the humidity for each one
                        foreach (BsonDocument reading in readings)
                        {
                            int co2Level = (int) reading[1]["co2"];

                            if (co2Level > 200)
                            {
                                // RenderSettings.fogColor.SetColor("_Color", new Color(153, 143, 199));
                                bool bConverted = ColorUtility.TryParseHtmlString("#998FC7", out newColor);
                                if (bConverted) {
                                    RenderSettings.fogColor = newColor;
                                    RenderSettings.fogDensity = 0.06F;
                                }
                                
                            }
                            else
                            {
                                bool bConverted = ColorUtility.TryParseHtmlString("#3b8ea5", out newColor);
                                if (bConverted) {
                                    RenderSettings.fogColor = newColor;
                                    RenderSettings.fogDensity = 0.05F;
                                }
                            }

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