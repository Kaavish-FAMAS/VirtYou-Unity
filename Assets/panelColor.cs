using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelColor : MonoBehaviour
{

    // Use t$$anonymous$$s for initialization
    void Start()
    {
        Image img = GameObject.Find("F1Plane").GetComponent<Image>();
        img.color = UnityEngine.Color.red;
    }

}