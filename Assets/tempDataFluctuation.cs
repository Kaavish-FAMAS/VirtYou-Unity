using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class tempDataFluctuation : MonoBehaviour
{

    Light lt;
    List<int> listA = new List<int>();
    int index = 0;
    
    // Read data from CSV
    void readData()
    {
        StreamReader strReader = new StreamReader("C:\\Users\\Asad\\Documents\\VirtYou-Unity\\Assets\\IOT-temp.csv");
        bool endOfFile = false;
        while (!endOfFile)
        {
            var dataString = strReader.ReadLine();
            if(dataString == null)
            {
                endOfFile = true;
                break;
            }
            var dataValues = dataString.Split(',');
            foreach (var item in dataValues)
            {
                if (item.ToString() != "temp")
                {
                    listA.Add(int.Parse(item));
                }
            }
        }
    }

    int singleDataPoint()
    {
        lt.color = Color.white;
        return listA[index];
    }

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
        readData();
    }

    // Update is called once per frame
    void Update()
    {
        int data = singleDataPoint();
        Debug.Log(data.ToString());
        if (data < 30)
        {
            lt.color = (Color.green / 2.0f) * 2;
        }
        else
        {
            lt.color = (Color.red / 2.0f) * 2;
        }
        index += 1;
    }
}
