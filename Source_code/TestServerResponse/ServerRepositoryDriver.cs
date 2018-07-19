using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Sockets;

using ServerPackage;


public class ServerRepositoryDriver : MonoBehaviour
{
    private void Start()
    {

        ServerRepository serverRepository = new ServerRepository();

        try
        {
            Debug.Log("getPasswordでkagawaのパスワードを取得");
            Debug.Log("getPassword出力: " + serverRepository.getPassword("kagawa") + "\n");


            Debug.Log("updatePasswordでkagawaのパスワードを更新");
            serverRepository.updatePassword("kagawa", "2018");
            Debug.Log("getPassword出力: " + serverRepository.getPassword("kagawa") + "\n");


            Debug.Log("getFriendsListでkagawaの友達リストを取得");
            Debug.Log("getFriendsList出力: " + serverRepository.getFriendsList("kagawa") + "\n");

            Debug.Log("insertFriendsListで友達リストに追加");
            serverRepository.insertFriendList("kagawa", "okazaki");
            Debug.Log("getFriendsList出力: " + serverRepository.getFriendsList("kagawa") + "\n");

            Debug.Log("getChatHistoryでkagawaとyoshidaのチャット履歴を取得");
            Debug.Log("getChatHistory出力: " + serverRepository.getChatHistory("kagawa", "yoshida") + "\n");


            Debug.Log("updateChatHistoryでkagawaとyoshidaのチャット履歴を更新");
            serverRepository.updateChatHistory("kagawa", "yoshida", "2018");
            Debug.Log("getChatHistory出力: " + serverRepository.getChatHistory("kagawa", "yoshida") + "\n");


            Debug.Log("getQuizでクイズの問題と解答を取得");
            Debug.Log("getQuiz出力: " + serverRepository.getQuiz("kagawa") + "\n");

            Debug.Log("updateQuizでクイズの問題と解答を更新");
            serverRepository.updateQuiz("kagawa", "kagawa");
            Debug.Log("getQuiz出力: " + serverRepository.getQuiz("kagawa") + "\n");


        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

    }

    private void Update()
    {
    }

}