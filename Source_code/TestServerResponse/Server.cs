﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Sockets;

using ServerPackage;



public class Server : MonoBehaviour
{

    private List<ServerClient> clients; // connected List
    private List<ServerClient> disconnectedList;

    public int port = 6543;
    private TcpListener server;
    private bool serverStarted;

    private void Start()
    {
        clients = new List<ServerClient>();
        disconnectedList = new List<ServerClient>();

        try
        {
            //IPAddress ip = IPAddress.Any;
            IPAddress ip = IPAddress.Parse("192.168.11.6");
            server = new TcpListener(ip, port);
            Debug.Log("IP: " + ip);

            server.Start();
            StartListening();
            serverStarted = true;
            Debug.Log("Server has been started on port: " + port.ToString());

        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }

    }

    private void Update()
    {
        if (!serverStarted)
            return;

        try
        {

            if (clients.Count > 0)
            {
                foreach (ServerClient c in clients)
                {

                    // Is the client still connected ?
                    if (!IsConnected(c.tcp))
                    {
                        c.tcp.Close();
                        Debug.Log(c.clientName+" has disconnected");
                        clients.Remove(c);
                        //disconnectedList.Add(c);
                        //continue;
                        break;
                    }
                    // Check for message from the client
                    else
                    {
                        NetworkStream s = c.tcp.GetStream();
                        if (s.DataAvailable)
                        {
                            StreamReader reader = new StreamReader(s, true);
                            string data = reader.ReadLine();

                            if (data != null)
                            {
                                OnIncomingData(c, data);
                            }
                        }
                    }

                    //for (int i = 0; i < (disconnectedList.Count - 1); i++)
                    //{
                    //    //Broadcast(disconnectedList[i].clientName + " has disconnected", clients);
                    //    clients.Remove(disconnectedList[i]);
                    //    disconnectedList.RemoveAt(i);
                    //}

                }
            }
        }
        catch (InvalidOperationException e)
        {
            Debug.Log(e.Message);
            Debug.Log("please ignore above erorrs");
        }

    }

    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    private bool IsConnected(TcpClient c)
    {

        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }
    }

    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening();

        // Send a message to everyone, say someone has connected
        //Broadcast(clients[clients.Count - 1].clientName + " has connected", clients);
        //Broadcast("%NAME", new List<ServerClient>() { clients[clients.Count - 1] });

        CastToClient("##NAME##", clients[clients.Count - 1]);
        //CastToClient(clients[clients.Count - 1].clientName, clients[clients.Count - 1]);

    }


    private void Broadcast(string data, List<ServerClient> cl)
    {
        foreach (ServerClient c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.Log("Write error: " + e.Message + " to client " + c.clientName);
            }
        }

    }

    private void OnIncomingData(ServerClient c, string data)
    {
        string response = string.Empty;
        Debug.Log("server accepted : " + data);
        if (data.Contains("##NAME|"))
        {
            c.clientName = data.Split('|')[1];
            CastToClient(c.clientName + " has connected", c);
            return;
        }

        ServerService serverService = new ServerService();
        response = serverService.requestSolver(data, c.clientName);

        Debug.Log("server response : " + response);

        if (!response.Contains("updateChatHistroy::success"))
        {
            CastToClient(response, c);
        }
        else
        {
            string nameChattingWith = response.Split('&')[1];
            string updater = c.clientName;

            data = "GET_CHAT_HISTORY%%" + nameChattingWith;
            Debug.Log("data : " + data);
            string diff_response = serverService.requestSolver(data, c.clientName);
            //Debug.Log("diff_res : " + diff_response);

            // Cast To  Updater and Whom Chatting with   
            foreach (ServerClient client in clients)
            {
                if (client.clientName == updater)
                {
                    CastToClient(diff_response, client);
                }
                else if (client.clientName == nameChattingWith)
                {
                    CastToClient(diff_response.Replace("ChatRecord", "ChatRecordFromTheOther"), client);
                }
            }
        }
    }

    private void CastToClient(string data, ServerClient client)
    {
        try
        {
            NetworkStream netStream = client.tcp.GetStream();
            StreamWriter writer = new StreamWriter(netStream);
            Debug.Log("Cast to clinet :" + data);
            writer.WriteLine(data);
            writer.Flush();
        }
        catch (Exception e)
        {
            Debug.Log("Write error: " + e.Message + " to client " + client.clientName);
        }
    }

}

public class ServerClient
{
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket)
    {
        clientName = "GUEST";
        tcp = clientSocket;
    }

}