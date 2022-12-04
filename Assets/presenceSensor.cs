using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class presenceSensor : MonoBehaviour
{
    Light lt;
    public int timerChecker;

    float RandomTemperature()
    {
        System.Random rd = new System.Random();
        float rand_num = rd.Next(20, 40);
        Debug.Log(rand_num);
        return rand_num;
    }

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timerChecker == 40)
        {
            float data = RandomTemperature();
            Debug.Log(data.ToString());
            timerChecker = 0;

            if (data < 25)
            {
                lt.color = (Color.green / 2.0f) * 2;
            }
            else
            {
                lt.color = (Color.red / 2.0f) * 2;
            }

        }
        else
        {
            timerChecker++;
        }
    }
}
