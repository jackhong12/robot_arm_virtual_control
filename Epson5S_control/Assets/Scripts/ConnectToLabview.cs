using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Net.Sockets;

public class ConnectToLabview : MonoBehaviour {
    private TcpServer sever;
    private string ip = "127.0.0.1", port = "8000";
    private string receiveMessage;


    void Start()
    {
        sever = new TcpServer(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, ip, port);
        sever.listen();
        sever.startConnect();
    }

    void FixedUpdate()
    {
        if (sever.isReceive())
        {
            receiveMessage = sever.getMessage();
            Debug.Log(receiveMessage);
        }
            
    }
}
