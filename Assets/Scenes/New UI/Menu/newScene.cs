using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(LoginBehavior.userRole);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("hit"))
        {
            Debug.Log("pressed hit");
        }
        // working mapped to joystick button 2
        else if (Input.GetButtonDown("B"))
        {
            Debug.Log("pressed B");
        }
        // working mapped to joystick button 1
        else if (Input.GetButtonDown("OnOff"))
        {
            Debug.Log("pressed OnOff");
        }
        // working mapped to joystick button 0
        else if (Input.GetButtonDown("Y"))
        {
            Debug.Log("pressed Y");
        }
    }
    
}
