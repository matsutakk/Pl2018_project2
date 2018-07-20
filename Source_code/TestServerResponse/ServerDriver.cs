using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class ServerDriver : MonoBehaviour
{
    private String[] clientName = new String[2];
    private bool[] socketReady = new bool[2];
    private TcpClient[] socket = new TcpClient[2];
    private NetworkStream[] stream = new NetworkStream[2];
    private StreamWriter[] writer = new StreamWriter[2];
    private StreamReader[] reader = new StreamReader[2];
    public Boolean[] isConnect = new Boolean[2];

    bool isCalled1 = false;
    bool isCalled2 = false;
    bool isCalled3 = false;
    bool isCalled4 = false;
    bool isCalled5 = false;
    bool isCalled6 = false;

    void Start()
    {

        isConnect[0] = false;
        isConnect[1] = false;

        clientName[0] = "User1";
        clientName[1] = "User2";


        IPAddress host = IPAddress.Parse("192.168.11.6");
        int port = 6543;

        StartCoroutine("Sleep");

        for (int i = 0; i < 2; i++)
        {
            try
            {
                socket[i] = new TcpClient();
                if (isConnect[i] != true)
                {
                    socket[i].Connect(host, port);
                }
                stream[i] = socket[i].GetStream();
                writer[i] = new StreamWriter(stream[i]);
                reader[i] = new StreamReader(stream[i]);
                socketReady[i] = true;

            }
            catch (Exception e)
            {
                Debug.Log("socket error : " + e.Message);
            }
        }




    }

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {

            if (socketReady[i])
            {
                if (stream[i].DataAvailable)
                {
                    string data = reader[i].ReadLine();
                    if (data != null)
                    {
                        if (data == "##NAME##")
                        {
                            StartCoroutine(Send(0.0f, "##NAME|" + clientName[i], i));
                           
                            isConnect[i] = true;
                            return;
                        }
                        Debug.Log("from server : " + data);
                    }
                }
            }
        }
        if (!isCalled1)
        {
            StartCoroutine(DelayMethod(80, () =>
            {
                Debug.Log("接続しているクライアントの確認");
            }));

            isCalled1 = true;
        }

        if (!isCalled2)
        {
            StartCoroutine(DelayMethod(80, () =>
            {
                StartCoroutine(Send(0.0f, "SHOW_CLIENTS", 0));
            }));

            isCalled2 = true;
        }

        if (!isCalled3)
        {
            StartCoroutine(DelayMethod(160, () =>
            {
                Debug.Log("User1の接続を切断");
            }));

            isCalled3 = true;
        }

        if (!isCalled4)
        {
            StartCoroutine(DelayMethod(160, () =>
        {
            Close(0);
        }));

            isCalled4 = true;
        }

        if (!isCalled5)
        {
            StartCoroutine(DelayMethod(180, () =>
        {
            Debug.Log("接続しているクライアントの確認");
        }));

            isCalled5 = true;
        }

        if (!isCalled6)
        {
            StartCoroutine(DelayMethod(180, () =>
        {
            StartCoroutine(Send(0.0f, "SHOW_CLIENTS", 1));
        }));

            isCalled6 = true;
        }

    }

    private IEnumerator Send(float delay, string data, int i)
    {
        if (!socketReady[i])
            yield return new WaitForSeconds(delay);

        yield return new WaitForSeconds(delay);

        writer[i].WriteLine(data);
        writer[i].Flush();

    }

    private void Close(int i)
    {
        socket[i].Close();
        socketReady[i] = false;
    }

    private IEnumerator Sleep()
    {
        yield return new WaitForSeconds(10);  //10秒待つ

    }
    private IEnumerator DelayMethod(int delayFrameCount, Action action)
    {
        for (var i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
        action();
    }
}
