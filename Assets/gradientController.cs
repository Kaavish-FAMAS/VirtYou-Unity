using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine.Networking;

public class gradientController : MonoBehaviour
{
    public float minValue = 16f;
    public float maxValue = 45f;
    private Material material;
    private Color startColor = Color.red;
    private Color endColor = Color.blue;

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
                            float t = Mathf.InverseLerp(minValue, maxValue, temperature);
                            
                            if (temperature > 35.1F)
                            {
                                // Interpolate between the start and end colors based on the temperature value
                                Color color = Color.Lerp(startColor, endColor, t);

                                if (material != null)
                                {
                                    material.color = color;
                                }
                            }
                            else
                            {
                                // Interpolate between the start and end colors based on the temperature value
                                Color color = Color.Lerp(endColor, startColor, t);

                                if (material != null)
                                {
                                    material.color = color;
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
