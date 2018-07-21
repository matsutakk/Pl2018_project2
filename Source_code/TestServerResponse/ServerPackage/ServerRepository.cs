using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.Diagnostics;

namespace ServerPackage
{

    public class ServerRepository
    {
        //////////////////////////////////////////////////////////////
        //*-------------------- About Password -----------------*//
        //////////////////////////////////////////////////////////////

        public string getPassword(string userName)
        {
            //string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "UserInfo.txt");
            string file_path = string.Join("/", new[] { Application.dataPath, "UsersData", userName, "UserInfo.txt" });

            if (!File.Exists(file_path))
            {
                //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName), file_path);
                createDirAndFile(string.Join("/", new[] { Application.dataPath, "UsersData", userName }), file_path);

                return "getPassword : the file doesn't exist";
            }

            string file_data = string.Empty;

            file_data = readFile(file_path);

            return file_data;
        }

        public string updatePassword(string userName, string new_password)
        {
            string file_data = getPassword(userName);

            string[] arr = file_data.Split(' ');

            arr[1] = new_password;

            string new_file_data = string.Join(" ", arr);

            //Debug.Log(new_file_data);

            //string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "UserInfo.txt");
            string file_path = string.Join("/", new[] { Application.dataPath, "UsersData", userName, "UserInfo.txt" });

            // overwrite
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(new_file_data);
            sw.Close();

            return string.Format("updateAccount : update {0}", file_path);
        }


        //////////////////////////////////////////////////////////////
        //*-------------------- About FriendsList -----------------*//
        //////////////////////////////////////////////////////////////

        public string getFriendsList(string userName)
        {
            //string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), Assets, "UsersData", userName, "FriendsList.txt");
            string file_path = string.Join("/", new[] { Application.dataPath, "UsersData", userName, "FriendsList.txt" });

            if (!File.Exists(file_path))
            {
                //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userName), file_path);
                createDirAndFile(string.Join("/", new[] { Application.dataPath, "UsersData", userName }), file_path);

                Debug.Log(string.Format("getFriendsList : {0} has been created", file_path));
            }

            string file_data = string.Empty;

            file_data = readFile(file_path);

            return file_data;
        }


        public string insertFriendList(string userName, string friendName)
        {
            string file_data = getFriendsList(userName);

            if (!file_data.Contains(friendName))
            {

                //string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), Assets, "UsersData", userName, "FriendsList.txt");
                string file_path = string.Join("/", new[] { Application.dataPath, "UsersData", userName, "FriendsList.txt" });

                if (!File.Exists(file_path))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userName), file_path);
                    createDirAndFile(string.Join("/", new[] { Application.dataPath, "UsersData", userName }), file_path);

                    Debug.Log(string.Format("insertFriendsList : {0} has been created", file_path));
                }
                StreamWriter sw = new StreamWriter(file_path, true);
                sw.Write(" " + friendName);
                sw.Close();
                return string.Format("insertFriendsList : {0} has been inserted in {1}'s FriendsList", friendName, userName);
            }
            else
            {
                Debug.Log(string.Format("insertFriendsList : {0} has already existed", friendName));
                return string.Format("insertFriendsList : {0} has already existed in {1}'s FriendsList", friendName, userName);
            }
        }


        //////////////////////////////////////////////////////////////
        //*-------------------- About ChatHistory -----------------*//
        //////////////////////////////////////////////////////////////

        public string getChatHistory(string userName, string userNameToTalk)
        {

            // friend to user
            //string file_path_friend_user = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userNameToTalk, "ChatHistory", userName + ".txt");
            string file_path_friend_user = string.Join("/", new[] { Application.dataPath, "UsersData", userNameToTalk, "ChatHistory", userName + ".txt" });

            if (!File.Exists(file_path_friend_user))
            {
                if (getFriendsList(userNameToTalk).Contains(userName))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()),  "Assets","UsersData", userNameToTalk, "ChatHistory"), file_path_friend_user);
                    createDirAndFile(string.Join("/", new[] { Application.dataPath, "UsersData", userNameToTalk, "ChatHistory" }), file_path_friend_user);


                    Debug.Log(string.Format("getChatHistroy (friend to user) : created chat history with {0}", userName));
                }
                else
                {
                    return string.Format("getChatHistroy (friend to user): {0} is not in {1}'s FriendsList", userName, userNameToTalk);
                }
            }

            // user to friend 
            //string file_path_user_friend = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userName, "ChatHistory", userNameToTalk + ".txt");
            string file_path_user_friend = string.Join("/", new[] { Application.dataPath, "UsersData", userName, "ChatHistory", userNameToTalk + ".txt" });

            if (!File.Exists(file_path_user_friend))
            {
                if (getFriendsList(userName).Contains(userNameToTalk))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "ChatHistory"), file_path_user_friend);
                    createDirAndFile(string.Join("/", new[] { Application.dataPath, "UsersData", userName, "ChatHistory" }), file_path_user_friend);

                    Debug.Log(string.Format("getChatHistroy (user to friend) : created chat history with {0}", userNameToTalk));
                }
                else
                {
                    return string.Format("getChatHistroy (user to friend) : {0} is not in {1}'s FriendsList", userNameToTalk, userName);
                }
            }

            string file_data = string.Empty;

            file_data = readFile(file_path_user_friend);

            return file_data;
        }


        public string updateChatHistory(string userName, string userNameToTalk, string chatContents)
        {
            // friend to user
            //string file_path_friend_user = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userNameToTalk, "ChatHistory", userName + ".txt");
            string file_path_friend_user = string.Join("/", new[] { Application.dataPath, "UsersData", userNameToTalk, "ChatHistory", userName + ".txt" });

            if (!File.Exists(file_path_friend_user))
            {
                if (getFriendsList(userNameToTalk).Contains(userName))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()),  "Assets","UsersData", userNameToTalk, "ChatHistory"), file_path_friend_user);
                    createDirAndFile(string.Join("/", new[] { Application.dataPath, "UsersData", userNameToTalk, "ChatHistory" }), file_path_friend_user);

                    Debug.Log(string.Format("updateChatHistory (friend to user) : created chat history with {0}", userName));
                }
                else
                {
                    Debug.Log(string.Format("updateChatHistory (friend to user): {0} is not in {1}'s FriendsList", userName, userNameToTalk));
                    return string.Format("updateChatHistory (friend to user): {0} is not in {1}'s FriendsList", userName, userNameToTalk);

                }
            }

            // user to friend 
            //string file_path_user_friend = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userName, "ChatHistory", userNameToTalk + ".txt");
            string file_path_user_friend = string.Join("/", new[] { Application.dataPath, "UsersData", userName, "ChatHistory", userNameToTalk + ".txt" });

            if (!File.Exists(file_path_user_friend))
            {
                if (getFriendsList(userName).Contains(userNameToTalk))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "ChatHistory"), file_path_user_friend);
                    createDirAndFile(string.Join("/", new[] { Application.dataPath, "UsersData", userName, "ChatHistory" }), file_path_user_friend);


                    Debug.Log(string.Format("updateChatHistory (user to friend) : created chat history with {0}", userNameToTalk));
                }
                else
                {
                    Debug.Log(string.Format("updateChatHistory (user to friend) : {0} is not in {1}'s FriendsList", userNameToTalk, userName));
                    return string.Format("updateChatHistory (user to friend) : {0} is not in {1}'s FriendsList", userNameToTalk, userName);
                }
            }

            // not overwrite

            // user to friend 
            StreamWriter sw1 = new StreamWriter(file_path_user_friend, true);
            sw1.Write(userName + "&&" + chatContents + "::");
            sw1.Close();

            // friend to user
            StreamWriter sw2 = new StreamWriter(file_path_friend_user, true);
            sw2.Write(userName + "&&" + chatContents + "::");
            sw2.Close();

            //Debug.Log("updateChatHistroy::success");
            //return string.Format("updateChatHistory : updated {0} \n and {1}", file_path_user_friend, file_path_friend_user);
            return ("updateChatHistroy::success&");
        }

        //////////////////////////////////////////////////////////////
        //*-------------------- About Quiz-------------------*////////
        //////////////////////////////////////////////////////////////

        public string getQuiz(string userName)
        {
            string file_path = string.Join("/", new[] { Application.dataPath, "UsersData", userName, "Quiz.txt" });

            //Debug.Log("getQuiz " + file_path);
            if (!File.Exists(file_path))
            {
                return "getQuiz : the file doesn't exist";
            }

            string file_data = string.Empty;

            file_data = readFile(file_path);

            return file_data;
        }


        // TODO : ユーザを指定
        public string createQuiz(string userName, string quizContents)
        {

            string[] contents_splitter = { "__" };

            string[] quiz_array = quizContents.Split(contents_splitter, StringSplitOptions.None);

            //
            if (quiz_array.Length != 5)
            {
                return string.Format("createQuiz : INVALID REQUEST : Request is not according to the protocol");
            }

            //string[] subFolders = Directory.GetFiles(string.Join("/", new[] { Application.dataPath, "UsersData", userName, "Quiz"}), "*.txt", SearchOption.AllDirectories);

            string file_path = string.Join("/", new[] { Application.dataPath, "UsersData", userName, "Quiz.txt" });

            if (File.Exists(file_path))
            {
                return "createQuiz : the file has already existed";
            }
            else
            {
                createDirAndFile(string.Join("/", new[] { Application.dataPath, "UsersData", userName }), file_path);
            }


            string new_file_data = string.Join("__", quiz_array);

            //Debug.Log(new_file_data);

            // overwrite
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(new_file_data);
            sw.Close();

            return string.Format("createQuiz : create {0}", file_path);
        }


        public string updateQuiz(string userName, string quizContents)
        {
            string new_file_data = quizContents;

            //Debug.Log(new_file_data);
            string file_path = string.Join("/", new[] { Application.dataPath, "UsersData", userName, "Quiz.txt" });

            // overwrite
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(new_file_data);
            sw.Close();

            return string.Format("updateQuiz : update {0}", file_path);
        }

        //////////////////////////////////////////////////////////////
        //*-------------------- Sub Methods -----------------*////////
        //////////////////////////////////////////////////////////////

        public void createDirAndFile(string directoryPath, string filePath)
        {
            Directory.CreateDirectory(directoryPath);
            using (FileStream fs = File.Create(filePath))
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        public string readFile(string path)
        {
            string file_data = "";
            try
            {
                FileInfo fi = new FileInfo(path);
                using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
                {
                    file_data = sr.ReadToEnd();
                }

               // Debug.Log("readFile : " + file_data);
            }
            catch (Exception e)
            {

                Debug.Log("The file could not be read:");
                Debug.Log(e.Message);
                file_data = "error";
            }
            return file_data;
        }

    }
}
