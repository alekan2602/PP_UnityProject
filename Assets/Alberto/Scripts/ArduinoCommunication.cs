using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoCommunication : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM3", 9600);
    public GameObject gameManagerObj;

    string selectionValue = "";

    bool canEnterDenyOnce = true;
    bool canEnterAcceptOnce = true;
    bool canEnterIdleOnce = true;

    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 666;
    }

    void Update()
    {
        string readValue = serialPort.ReadLine();

        if (readValue == "0" && this.canEnterIdleOnce == true)
        {
            gameManagerObj.GetComponent<scr_GameManager>().playerSelection = 0;
            selectionValue = "Ninguna";
            SendSignal('N');

            this.canEnterAcceptOnce = true;
            this.canEnterDenyOnce = true;
            this.canEnterIdleOnce = false;
        }
        else if (readValue == "1" && this.canEnterAcceptOnce == true)
        {
            gameManagerObj.GetComponent<scr_GameManager>().playerSelection = 1;
            selectionValue = "Aceptado";
            SendSignal('A');

            this.canEnterAcceptOnce = false;
            this.canEnterDenyOnce = true;
            this.canEnterIdleOnce = true;
        }
        else if (readValue == "2" && this.canEnterDenyOnce == true)
        {
            gameManagerObj.GetComponent<scr_GameManager>().playerSelection = 2;
            selectionValue = "Denegado";
            SendSignal('D');

            this.canEnterAcceptOnce = true;
            this.canEnterDenyOnce = false;
            this.canEnterIdleOnce = true;
        }

        Debug.Log(readValue);
    }

    private void OnGUI()
    {
        string selectionText = "Selección: " + selectionValue;
        GUI.Label(new Rect(10, 10, 300, 100), selectionText);
    }

    void SendSignal(char signal)
    {
        string convertedSignal = Convert.ToString(signal);

        if (serialPort.IsOpen)
        {
            serialPort.Write(convertedSignal);
        }
    }
}
