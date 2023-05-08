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
    public GameObject firstPlane;
    public GameObject secondPlane;
    public GameObject thirdPlane;
    public GameObject fourthPlane;

    private bool isMeshColored = false;

    void Start()
    {
        string apiUrl = "https://flask-mongo-backend-ar230500-famas.vercel.app/getsensor/";
        
        // Get the material component attached to the game object
        material = GetComponent<Renderer>().material;

        firstPlane.GetComponent<MeshRenderer>().enabled = false;
        secondPlane.GetComponent<MeshRenderer>().enabled = false;
        thirdPlane.GetComponent<MeshRenderer>().enabled = false;
        fourthPlane.GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(GradientController(apiUrl));
    }

    void Update()
    {
        if (Input.GetButtonDown("hit") || Input.GetKeyDown(KeyCode.Space))
        {
            isMeshColored = !isMeshColored;

            firstPlane.GetComponent<MeshRenderer>().enabled = isMeshColored;
            secondPlane.GetComponent<MeshRenderer>().enabled = isMeshColored;
            thirdPlane.GetComponent<MeshRenderer>().enabled = isMeshColored;
            fourthPlane.GetComponent<MeshRenderer>().enabled = isMeshColored;
        }
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

                    if (isMeshColored && readings != null && readings.Count > 0)
                    {
                        firstPlane.GetComponent<MeshRenderer>().enabled = true;
                        secondPlane.GetComponent<MeshRenderer>().enabled = true;
                        thirdPlane.GetComponent<MeshRenderer>().enabled = true;
                        fourthPlane.GetComponent<MeshRenderer>().enabled = true;

                        // Iterate over the readings and display the humidity for each one
                        foreach (BsonDocument reading in readings)
                        {
                            float temperature = (float) reading[1]["temperature"].ToDouble();
                            float t = Mathf.InverseLerp(minValue, maxValue, temperature);
                                    
                            if (temperature > 26.0F)
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
                        firstPlane.GetComponent<MeshRenderer>().enabled = false;
                        secondPlane.GetComponent<MeshRenderer>().enabled = false;
                        thirdPlane.GetComponent<MeshRenderer>().enabled = false;
                        fourthPlane.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
        }
    }
}