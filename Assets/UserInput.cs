﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{


    /*
    
     Good luck folks! If you say to thanks, my LinkedIn profile link below
     
     https://www.linkedin.com/in/malisasmaz
     
     you can +rep my skills thanks!
     
     VR_LIONEYE HAVE 4 MOD; 
          FOR MOD CHANGE PRESS @-A   -Mouse mod ** my codes not work on this mode
                               @-B   -Horizontal bluetooth controller mode
                               @-C   -Vertical bluetooth controller mode
                               @-D   -Inactive mod also don't work nothing
         for activate or changing modes press @ and other button what you need same time until see light
     
    joystick button 3           - A     -X1

    joystick button 0           - B     -X2

    joystick button 2           - C

    joystick button 1           - D

    joystick button 11          - OnOff //only work @C mod 

    */


    public GameObject myGo;
    public Text myText;

    float roll, pitch;


    void Update()
    {
        roll = Input.GetAxis("Horizontal");     //joystick horizontal
        pitch = Input.GetAxis("Vertical");      //joystick vertical

        myGo.transform.position += new Vector3(roll * 0.1f, pitch * 0.1f, 0);


        //Bluetooth Controller Joystick
        if (Input.GetAxis("Vertical") > 0)
        {
            ButtonName("Up Pressed");
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            ButtonName("Down Pressed");
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            ButtonName("Right Pressed");
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            ButtonName("Left Pressed");
        }




        // Bluetooth Controller Buttons VR_Lioneye
        // working mapped to joystick button 3 (front button of controller)
        if (Input.GetButtonDown("hit"))
        {
            ButtonName("hit");
            Debug.Log("pressed hit");
        }
        // working mapped to joystick button 2
        if (Input.GetButtonDown("B"))
        {
            ButtonName("B");
            Debug.Log("pressed B");
        }
        // working mapped to joystick button 1
        if (Input.GetButtonDown("OnOff"))
        {
            ButtonName("OnOff");
            Debug.Log("pressed OnOff");
        }
        // working mapped to joystick button 0
        if (Input.GetButtonDown("Y"))
        {
            ButtonName("Y");
            Debug.Log("pressed Y");
        }
        if (Input.GetButtonDown("extra"))
        {
            ButtonName("extra");
            Debug.Log("pressed extra");
        }
        

    }

    public void ButtonName(string ButtonName)
    {
        myText.text = ButtonName;
    }
}
