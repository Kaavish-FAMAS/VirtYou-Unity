using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class relayControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Playground");
    }

    // Update is called once per frame
    void Update()
    {
        if (LoginBehavior.userRole == "admin")
        {
            // working mapped to joystick button 2
            if (Input.GetButtonDown("B"))
            {
                Debug.Log("pressed B");
            }
            else if (Input.GetButtonDown("Y"))
            {
                // do something
            }
        }
    }
}
