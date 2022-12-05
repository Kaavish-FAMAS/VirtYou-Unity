using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueToRed : MonoBehaviour
{
    Light lt;
    Color startColor = Color.blue;
    Color endColor = Color.yellow;
    float speed = 600.0f;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
        StartCoroutine(ChangeEngineColour());
    }

    private IEnumerator ChangeEngineColour()
    {
        float tick = 0f;
        while (lt.color != endColor)
        {
            tick += Time.deltaTime * speed;
            lt.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * 0.5f, 1));
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
