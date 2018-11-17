/*****************************************************
 * tcp api: https://docs.microsoft.com/zh-tw/dotnet/api/system.net.ipaddress?view=netframework-4.7.2
 * Date: 2018/11/17
 *****************************************************/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;

public class TcpServer{
    //socket data
    private Socket serverSocket;
    private Socket clientSocket;
    private string ip, port;
    private bool isClientConnect;

    //messge data
    readonly private int receiveMessageSize = 256;   //每次讀取大小
    private string receiveMessage;
    private bool isReceiveNewMessage;    //判斷是否收到新的字

    //thread
    private Thread threadConnect;   //等待連結
    private Thread threadReceive;   //接收字串

    //constructor
    public TcpServer(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, string ip, string port)
    {
        serverSocket = new Socket(addressFamily, socketType, protocolType);
        this.ip = ip;
        this.port = port;
        isReceiveNewMessage = false;
        isClientConnect = false;
    }

    //client連線的thread
    public void startConnect()
    {
        threadConnect = new Thread(waitingClient);
        threadConnect.IsBackground = true;  //設定為背景執行續，當程式關閉時會自動結束
        threadConnect.Start();
    }

    public void listen()
    {
        try
        {
            //使 Socket 與本機端點建立關聯。
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), int.Parse(port)));
            serverSocket.Listen(1); //最多一次接受多少人連線
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public bool isConnect()
    {
        return isClientConnect;
    }

    public bool isReceive()
    {
        
        //還沒連線回傳false
        if (!isClientConnect)
            return false;

        //還沒收到新的字串
        if (!isReceiveNewMessage)
            return false;

        //收到新的字串
        threadReceive = new Thread(receive);
        threadReceive.IsBackground = true;
        threadReceive.Start();
        isReceiveNewMessage = false;
        return true;
    }

    public void send(string message)
    {
        if (message == null)
            return;

        if (clientSocket.Connected)
            clientSocket.Send(Encoding.ASCII.GetBytes(message));
    }

    public string getMessage()
    {
        return receiveMessage;
    }

    //等待client連線
    private void waitingClient()
    {
        try
        {
            Debug.Log("waiting connect");
            clientSocket = serverSocket.Accept();
            isClientConnect = true;

            //start get message
            threadReceive = new Thread(receive);
            threadReceive.IsBackground = true;
            threadReceive.Start();
            isReceiveNewMessage = false;
            Debug.Log("connect ok");
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    //receive thread function
    private void receive()
    {
        if (clientSocket.Connected)
        {
            //Debug.Log("receive...");
            byte[] bytes = new byte[receiveMessageSize];
            clientSocket.Receive(bytes);
            receiveMessage = Encoding.ASCII.GetString(bytes);
            isReceiveNewMessage = true;
        }
    }
}
