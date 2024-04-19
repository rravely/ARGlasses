using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;
using System;



public class SensorReadManager : MonoBehaviour
{
    public static SensorReadManager instance;

    public SerialPort serial = new SerialPort("COM7", 115200);

    public float fPtich = 0.0f;
    public float fRoll = 0.0f;
    public float fYaw = 0.0f;

    public int iPtich = 0;
    public int iRoll = 0;
    public int iYaw = 0;
    public int b_iYaw = 0;
    public static int iYawCompValue = 0;
    bool bYawDateCompDone = false;

    void Start()
    {
        if (instance != null) Destroy(instance);
        instance = this;

        serial.ReadTimeout = 10;
        serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (serial.IsOpen)
            {
                serialEvent(serial);
            }
        }

        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    void serialEvent(SerialPort myPort)
    {
        string inputText = myPort.ReadLine(); //the ascii value of the "|" character

        if (inputText != null)
        {
            string[] split_text;
            split_text = inputText.Split(',');

            string[] temp_text;
            temp_text = split_text[0].Split('*');

            fRoll = float.Parse(temp_text[1]);
            fPtich = float.Parse(split_text[1]);
            fYaw = float.Parse(split_text[2]);

            iRoll = (int)fRoll;
            iPtich = (int)fPtich;
            iYaw = (int)fYaw;


            if (bYawDateCompDone == false)
            {
                iYawCompValue = iYaw;
                bYawDateCompDone = true;
            }

            else
            {
                iYaw = iYaw + Math.Abs(iYawCompValue);
            }


        }
    }

    void OnApplicationQuit()
    {
        serial.Close();
    }
}
