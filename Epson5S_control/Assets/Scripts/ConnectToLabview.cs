using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Net.Sockets;

public class ConnectToLabview : MonoBehaviour {
    private TcpServer sever;
    private string ip = "127.0.0.1", port = "8000";
    private string receiveMessage;
    public float var1, var2;

    void Start()
    {   
        /*
        var1 = 0;
        var2 = 0;
        sever = new TcpServer(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, ip, port);
        sever.listen();
        sever.startConnect();
        */
    }

    void FixedUpdate()
    {   
        /*
        if (sever.isReceive())
        {
            receiveMessage = sever.getMessage();
            //Debug.Log(receiveMessage);
        }
        setTwoVariable();
        */
    }

    void setTwoVariable()
    {
        int findComma = receiveMessage.IndexOf(",");
        int findEnd = receiveMessage.IndexOf("E");
        string vstr1 = receiveMessage.Substring(1, findComma);
        string vstr2 = receiveMessage.Substring(findComma + 1, findEnd);
        var1 = float.Parse(vstr1);
        var2 = float.Parse(vstr2);
        Debug.Log("var1: " + var1 + "var2: " + var2);
    }
}
