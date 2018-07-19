using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Sockets;

using ServerPackage;
public class ServerServiceDriver : MonoBehaviour
{
    private void Start()
    {
        ServerService serverService = new ServerService();
        try
        {
            Debug.Log("GET_PASSWORDでkagawaのパスワードを取得");
            Debug.Log("出力: " + serverService.requestSolver("GET_PASSWORD", "kagawa") + "\n");


            Debug.Log("UPDATE_PASSWORD%%2018でkagawaのパスワードを更新");
            serverService.requestSolver("UPDATE_PASSWORD%%2018", "kagawa");
            Debug.Log("出力: " + serverService.requestSolver("GET_PASSWORD", "kagawa") + "\n");


            Debug.Log("GET_FRIENDSLISTでkagawaの友達リストを取得");
            Debug.Log("出力: " + serverService.requestSolver("GET_FRIENDSLIST", "kagawa") + "\n");

            Debug.Log("INSERT_FRIENDSLIST%%okazakiで友達リストに追加");
            serverService.requestSolver("INSERT_FRIENDSLIST%%okazaki", "kagawa");
            Debug.Log("出力: " + serverService.requestSolver("GET_FRIENDSLIST", "kagawa") + "\n");

            Debug.Log("GET_CHAT_HISTORY%%yoshidaでkagawaとyoshidaのチャット履歴を取得");
            Debug.Log("出力: " + serverService.requestSolver("GET_CHAT_HISTORY%%yoshida", "kagawa") + "\n");


            Debug.Log("UPDATE_CHAT_HISTORY%%yoshida%%チャットの内容でkagawaとyoshidaのチャット履歴を更新");
            serverService.requestSolver("UPDATE_CHAT_HISTORY%%yoshida%%2018", "kagawa");
            Debug.Log("出力: " + serverService.requestSolver("GET_CHAT_HISTORY%%yoshida", "kagawa") + "\n");


            Debug.Log("GET_QUIZ%%kagawaでクイズの問題と解答を取得");
            Debug.Log("出力: " + serverService.requestSolver("GET_QUIZ%%kagawa", "kagawa") + "\n");

            Debug.Log("UPDATE_QUIZ%%問題__答1__答2__答3でクイズの問題と解答を更新");
            serverService.requestSolver("UPDATE_QUIZ%%number?__11__10__100", "kagawa");
            Debug.Log("出力: " + serverService.requestSolver("GET_QUIZ%%kagawa", "kagawa") + "\n");

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
