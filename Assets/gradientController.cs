using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gradientController : MonoBehaviour
{
    //public DatabaseManager databaseManager;
    public float minValue = 16f;
    public float maxValue = 45f;
    public float temperature = 18f;
    public Color startColor = Color.blue;
    public Color endColor = Color.red;

    private Material material;

    void Start()
    {
        // Get the material component attached to the game object
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Get the temperature value from the database
        //float temperature = databaseManager.GetTemperature();

        // Convert the temperature value to a range between 0 and 1
        float t = Mathf.InverseLerp(minValue, maxValue, temperature);

        // Interpolate between the start and end colors based on the temperature value
        Color color = Color.Lerp(startColor, endColor, t);

        // Set the color of the material if it exists
        if (material != null)
        {
            Debug.Log("NOT NULL");
            material.color = color;
        } else
        {
            Debug.Log("NULL");
        }

    }
}
