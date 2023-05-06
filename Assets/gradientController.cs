using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.Networking;

public class gradientController : MonoBehaviour
{
    private Material material;
    private Color newColor;

    void Start()
    {
        string apiUrl = "https://flask-mongo-backend-ar230500-famas.vercel.app/getsensor/";
        
        // Get the material component attached to the game object
        material = GetComponent<Renderer>().material;

        StartCoroutine(GradientController(apiUrl));
    }

    private IEnumerator GradientController(string apiUrl)
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
                    Debug.Log(readings);

                    if (readings != null && readings.Count > 0)
                    {

                        // Iterate over the readings and display the humidity for each one
                        foreach (BsonDocument reading in readings)
                        {
                            float temperature = (float) reading[1]["temperature"].ToDouble();
                            
                            if (temperature > 28.5F)
                            {
                                if (material != null)
                                {
                                    bool bConverted = ColorUtility.TryParseHtmlString("#fe4e00", out newColor);
                                    if (bConverted) {
                                        material.color = newColor;
                                    }
                                }
                            }
                            else
                            {
                                if (material != null)
                                {
                                    bool bConverted = ColorUtility.TryParseHtmlString("#5fbff9", out newColor);
                                    if (bConverted) {
                                        material.color = newColor;
                                    }
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
