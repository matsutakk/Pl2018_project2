using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FileOperation
{
    class ServerService
    {
        ServerRepository serverRepository = new ServerRepository();

        public string requestSolver(string request, string clientName)
        {
            string response = string.Empty;
            string comment = string.Empty;
            string[] request_splitter = {"%%"};
            string[] request_array = request.Split(request_splitter, StringSplitOptions.None);

            // check request valid 
            comment = checkAndCommentForRequest(request_array);

            if(comment=="OK")
            {
                switch (request_array[0])
                {
                    case "GET_PASSWORD":
                        response = serverRepository.getPassword(clientName);
                        break;

                    case "UPDATE_PASSWORD":
                        response = serverRepository.updatePassword(clientName, request_array[1]);
                        break;

                    case "GET_FRIENDSLIST":
                        response = serverRepository.getFriendsList(clientName);
                        break;

                    case "INSERT_FRIENDSLIST":
                        response = serverRepository.insertFriendList(clientName, request_array[1]);
                        break;

                    case "GET_CHAT_HISTORY":
                        response = serverRepository.getChatHistroy(clientName, request_array[1]);
                        break;

                    case "UPDATE_CHAT_HISTORY":
                        response = serverRepository.updateChatHistory(clientName, request_array[1], request_array[2]);
                        break;

                    case "GET_QUIZ":
                        response = serverRepository.getQuiz(clientName);
                        break;
                }
                return response;
            }
            else
            {
                return comment;
            }
        }

        private string checkAndCommentForRequest(string[] request_array)
        {
            string comment = string.Empty;

            switch (request_array[0])
            {
                case "GET_PASSWORD":
                    if (request_array.Length == 1)
                        comment = "OK";
                    else comment = "INVALID REQUEST : Request is not according to the protocol";
                    break;

                case "UPDATE_PASSWORD":
                    if (request_array.Length == 2)
                        comment = "OK";
                    else comment = "INVALID REQUEST : Request is not according to the protocol";
                    break;

                case "GET_FRIENDSLIST":
                    if (request_array.Length == 1)
                        comment = "OK";
                    break;

                case "INSERT_FRIENDSLIST":
                    if (request_array.Length == 2)
                        comment = "OK";
                    else comment = "INVALID REQUEST : Request is not according to the protocol";
                    break;

                case "GET_CHAT_HISTORY":
                    if (request_array.Length == 2)
                        comment = "OK";
                    else comment = "INVALID REQUEST : Request is not according to the protocol";
                    break;

                case "UPDATE_CHAT_HISTORY":
                    if (request_array.Length == 3)
                        comment = "OK";
                    else comment = "%% CONTAIN ERROR : Request contains %% in passage";
                    break;

                case "GET_QUIZ":
                    if (request_array.Length == 1)
                        comment = "OK";
                    else comment = "INVALID REQUEST : Request is not according to the protocol";
                    break;

                default:
                    comment = "INVALID REQUEST : Request is not according to the protocol";
                    break;
            }

            return comment;
        }


        static void Main(string[] args)
        {
            ServerService ss = new ServerService();
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));

            Debug.WriteLine(ss.requestSolver("UPDATE_CHAT_HISTORY%%yoshida%%ssssss", "agawa"));
            Debug.WriteLine(ss.requestSolver("GET_CHAT_HISTORY%%yoshida%%eeeee", "kagawa"));

            // test

        }
    }
}
