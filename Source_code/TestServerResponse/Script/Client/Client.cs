using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

public class Client : MonoBehaviour
{
    public string clientName;

    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    public void ConnectedToServer()
    {
        // if already connected igonore this function
        if (socketReady)
            return;

        //Default host/ port values
        string host = "127.0.0.1";
        int port = 6321;

        // Overwrite default host / post values, if there is something in those boxes
        //string h;
        //int p;
        
        // create the socket
        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;

        }
        catch (Exception e)
        {
            Debug.Log("socket error : " + e.Message);
        }

    }

    private void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    OnIncomingData(data);
                }
            }
        }
    }
    private void OnIncomingData(string data)
    {
        Debug.Log("from server : " + data);

        if (data == "##NAME##")
        {
            Send("##NAME|" + clientName);
            return;
        }

        GameObject.Find("Response").GetComponent<Text>().text = data;
    }

    private void Send(string data)
    {
        if (!socketReady)
            return;

        writer.WriteLine(data);
        writer.Flush();

    }

    public void OnSendButton()
    {
        string message = GameObject.Find("SendInput").GetComponent<InputField>().text;
        Debug.Log(clientName + " has sent the followinng message from client: " + message);
        Send(message);
        
    }

    private void CloseSocket()
    {
        if (!socketReady)
            return;

        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    private void OnDisable()
    {
        CloseSocket();
    }


}
